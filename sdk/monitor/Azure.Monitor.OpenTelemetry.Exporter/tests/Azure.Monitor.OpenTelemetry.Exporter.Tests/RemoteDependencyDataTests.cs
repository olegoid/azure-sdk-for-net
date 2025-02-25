﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics;
using System.Globalization;

using Azure.Monitor.OpenTelemetry.Exporter.Internals;
using Azure.Monitor.OpenTelemetry.Exporter.Models;

using OpenTelemetry.Trace;

using Xunit;

namespace Azure.Monitor.OpenTelemetry.Exporter.Tests
{
    public class RemoteDependencyDataTests
    {
        private const string ActivitySourceName = "RemoteDependencyDataTests";
        private const string ActivityName = "RemoteDependencyDataActivity";

        static RemoteDependencyDataTests()
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            Activity.ForceDefaultIdFormat = true;

            var listener = new ActivityListener
            {
                ShouldListenTo = _ => true,
                Sample = (ref ActivityCreationOptions<ActivityContext> options) => ActivitySamplingResult.AllData,
            };

            ActivitySource.AddActivityListener(listener);
        }

        [Theory]
        [InlineData("mssql")]
        [InlineData("redis")]
        public void ValidateDBDependencyType(string dbSystem)
        {
            using ActivitySource activitySource = new ActivitySource(ActivitySourceName);
            using var activity = activitySource.StartActivity(
                ActivityName,
                ActivityKind.Server,
                parentContext: new ActivityContext(ActivityTraceId.CreateRandom(), ActivitySpanId.CreateRandom(), ActivityTraceFlags.Recorded),
                startTime: DateTime.UtcNow);

            Assert.NotNull(activity);
            activity.SetTag(SemanticConventions.AttributeDbSystem, dbSystem);

            var activityTagsProcessor = TraceHelper.EnumerateActivityTags(activity);

            var remoteDependencyDataType = new RemoteDependencyData(2, activity, ref activityTagsProcessor).Type;
            var expectedType = RemoteDependencyData.s_sqlDbs.Contains(dbSystem) ? "SQL" : dbSystem;

            Assert.Equal(expectedType, remoteDependencyDataType);
        }

        [Fact]
        public void DependencyTypeisSetToInProcForInternalSpan()
        {
            using ActivitySource activitySource = new ActivitySource(ActivitySourceName);
            using var activity = activitySource.StartActivity("Activity", ActivityKind.Internal);

            Assert.NotNull(activity);
            var activityTagsProcessor = TraceHelper.EnumerateActivityTags(activity);

            var remoteDependencyDataType = new RemoteDependencyData(2, activity, ref activityTagsProcessor).Type;

            Assert.Equal("InProc", remoteDependencyDataType);
        }

        [Fact]
        public void DependencyTypeisSetToAzNamespaceValueForNonInternalSpan()
        {
            using ActivitySource activitySource = new ActivitySource(ActivitySourceName);
            using var activity = activitySource.StartActivity("Activity", ActivityKind.Client);
            activity?.AddTag("az.namespace", "DemoAzureResource");

            Assert.NotNull(activity);
            var activityTagsProcessor = TraceHelper.EnumerateActivityTags(activity);

            var remoteDependencyData = new RemoteDependencyData(2, activity, ref activityTagsProcessor);

            Assert.Equal("DemoAzureResource", remoteDependencyData.Type);
            Assert.Equal("DemoAzureResource", remoteDependencyData.Properties["az.namespace"]);
        }

        [Fact]
        public void ValidateHttpRemoteDependencyData()
        {
            using ActivitySource activitySource = new ActivitySource(ActivitySourceName);
            using var activity = activitySource.StartActivity(
                ActivityName,
                ActivityKind.Client,
                parentContext: new ActivityContext(ActivityTraceId.CreateRandom(), ActivitySpanId.CreateRandom(), ActivityTraceFlags.Recorded),
                startTime: DateTime.UtcNow);
            Assert.NotNull(activity);
            activity.Stop();

            var httpUrl = "https://www.foo.bar/search";
            activity.SetStatus(Status.Ok);
            activity.SetTag(SemanticConventions.AttributeHttpMethod, "GET");
            activity.SetTag(SemanticConventions.AttributeHttpUrl, httpUrl); // only adding test via http.url. all possible combinations are covered in AzMonListExtensionsTests.
            activity.SetTag(SemanticConventions.AttributeHttpStatusCode, null);

            var activityTagsProcessor = TraceHelper.EnumerateActivityTags(activity);

            var remoteDependencyData = new RemoteDependencyData(2, activity, ref activityTagsProcessor);

            Assert.Equal("GET /search", remoteDependencyData.Name);
            Assert.Equal(activity.Context.SpanId.ToHexString(), remoteDependencyData.Id);
            Assert.Equal(httpUrl, remoteDependencyData.Data);
            Assert.Equal("0", remoteDependencyData.ResultCode);
            Assert.Equal(activity.Duration.ToString("c", CultureInfo.InvariantCulture), remoteDependencyData.Duration);
            Assert.Equal(activity.GetStatus() != Status.Error, remoteDependencyData.Success);
            Assert.True(remoteDependencyData.Properties.Count == 0);
            Assert.True(remoteDependencyData.Measurements.Count == 0);
        }

        [Fact]
        public void ValidateDbRemoteDependencyData()
        {
            using ActivitySource activitySource = new ActivitySource(ActivitySourceName);
            using var activity = activitySource.StartActivity(
                ActivityName,
                ActivityKind.Client,
                parentContext: new ActivityContext(ActivityTraceId.CreateRandom(), ActivitySpanId.CreateRandom(), ActivityTraceFlags.Recorded),
                startTime: DateTime.UtcNow);
            Assert.NotNull(activity);
            activity.Stop();

            activity.SetStatus(Status.Ok);
            activity.SetTag(SemanticConventions.AttributeDbSystem, "mssql");
            activity.SetTag(SemanticConventions.AttributePeerService, "localhost"); // only adding test via peer.service. all possible combinations are covered in AzMonListExtensionsTests.
            activity.SetTag(SemanticConventions.AttributeDbStatement, "Select * from table");

            var activityTagsProcessor = TraceHelper.EnumerateActivityTags(activity);

            var remoteDependencyData = new RemoteDependencyData(2, activity, ref activityTagsProcessor);

            Assert.Equal(ActivityName, remoteDependencyData.Name);
            Assert.Equal(activity.Context.SpanId.ToHexString(), remoteDependencyData.Id);
            Assert.Equal("Select * from table", remoteDependencyData.Data);
            Assert.Null(remoteDependencyData.ResultCode);
            Assert.Equal(activity.Duration.ToString("c", CultureInfo.InvariantCulture), remoteDependencyData.Duration);
            Assert.Equal(activity.GetStatus() != Status.Error, remoteDependencyData.Success);
            Assert.True(remoteDependencyData.Properties.Count == 0);
            Assert.True(remoteDependencyData.Measurements.Count == 0);
        }

        [Fact]
        public void HttpDependencyNameIsActivityDisplayNameByDefault()
        {
            using ActivitySource activitySource = new ActivitySource(ActivitySourceName);
            using var activity = activitySource.StartActivity(
                ActivityName,
                ActivityKind.Client,
                parentContext: new ActivityContext(ActivityTraceId.CreateRandom(), ActivitySpanId.CreateRandom(), ActivityTraceFlags.Recorded),
                startTime: DateTime.UtcNow);

            Assert.NotNull(activity);
            activity.SetTag(SemanticConventions.AttributeHttpMethod, "GET");

            activity.DisplayName = "HTTP GET";

            var activityTagsProcessor = TraceHelper.EnumerateActivityTags(activity);

            var remoteDependencyDataName = new RemoteDependencyData(2, activity, ref activityTagsProcessor).Name;

            Assert.Equal(activity.DisplayName, remoteDependencyDataName);
        }
    }
}

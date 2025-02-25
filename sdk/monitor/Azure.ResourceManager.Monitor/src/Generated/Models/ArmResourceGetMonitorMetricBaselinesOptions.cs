// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.Monitor.Models
{
    /// <summary> The ArmResourceGetMonitorMetricBaselinesOptions. </summary>
    public partial class ArmResourceGetMonitorMetricBaselinesOptions
    {
        /// <summary> Initializes a new instance of ArmResourceGetMonitorMetricBaselinesOptions. </summary>
        public ArmResourceGetMonitorMetricBaselinesOptions()
        {
        }

        /// <summary> The names of the metrics (comma separated) to retrieve. Special case: If a metricname itself has a comma in it then use %2 to indicate it. Eg: &apos;Metric,Name1&apos; should be **&apos;Metric%2Name1&apos;**. </summary>
        public string Metricnames { get; set; } = null;

        /// <summary> Metric namespace to query metric definitions for. </summary>
        public string Metricnamespace { get; set; } = null;

        /// <summary> The timespan of the query. It is a string with the following format &apos;startDateTime_ISO/endDateTime_ISO&apos;. </summary>
        public string Timespan { get; set; } = null;

        /// <summary> The interval (i.e. timegrain) of the query. </summary>
        public TimeSpan? Interval { get; set; } = null;

        /// <summary> The list of aggregation types (comma separated) to retrieve. </summary>
        public string Aggregation { get; set; } = null;

        /// <summary> The list of sensitivities (comma separated) to retrieve. </summary>
        public string Sensitivities { get; set; } = null;

        /// <summary> The **$filter** is used to reduce the set of metric data returned. Example: Metric contains metadata A, B and C. - Return all time series of C where A = a1 and B = b1 or b2 **$filter=A eq &apos;a1&apos; and B eq &apos;b1&apos; or B eq &apos;b2&apos; and C eq &apos;*&apos;** - Invalid variant: **$filter=A eq &apos;a1&apos; and B eq &apos;b1&apos; and C eq &apos;*&apos; or B = &apos;b2&apos;** This is invalid because the logical or operator cannot separate two different metadata names. - Return all time series where A = a1, B = b1 and C = c1: **$filter=A eq &apos;a1&apos; and B eq &apos;b1&apos; and C eq &apos;c1&apos;** - Return all time series where A = a1 **$filter=A eq &apos;a1&apos; and B eq &apos;*&apos; and C eq &apos;*&apos;**. Special case: When dimension name or dimension value uses round brackets. Eg: When dimension name is **dim (test) 1** Instead of using $filter= &quot;dim (test) 1 eq &apos;*&apos; &quot; use **$filter= &quot;dim %2528test%2529 1 eq &apos;*&apos; &quot;** When dimension name is **dim (test) 3** and dimension value is **dim3 (test) val** Instead of using $filter= &quot;dim (test) 3 eq &apos;dim3 (test) val&apos; &quot; use **$filter= &quot;dim %2528test%2529 3 eq &apos;dim3 %2528test%2529 val&apos; &quot;**. </summary>
        public string Filter { get; set; } = null;

        /// <summary> Allows retrieving only metadata of the baseline. On data request all information is retrieved. </summary>
        public MonitorResultType? ResultType { get; set; } = null;
    }
}

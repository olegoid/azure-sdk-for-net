// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;
using Azure.ResourceManager.HybridData;

namespace Azure.ResourceManager.HybridData.Models
{
    internal partial class DataStoreList
    {
        internal static DataStoreList DeserializeDataStoreList(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            Optional<IReadOnlyList<HybridDataStoreData>> value = default;
            Optional<string> nextLink = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("value"u8))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    List<HybridDataStoreData> array = new List<HybridDataStoreData>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(HybridDataStoreData.DeserializeHybridDataStoreData(item));
                    }
                    value = array;
                    continue;
                }
                if (property.NameEquals("nextLink"u8))
                {
                    nextLink = property.Value.GetString();
                    continue;
                }
            }
            return new DataStoreList(Optional.ToList(value), nextLink.Value);
        }
    }
}

// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.MachineLearning.Models
{
    /// <summary> Azure Data Lake Gen2 datastore configuration. </summary>
    public partial class MachineLearningAzureDataLakeGen2Datastore : MachineLearningDatastoreProperties
    {
        /// <summary> Initializes a new instance of MachineLearningAzureDataLakeGen2Datastore. </summary>
        /// <param name="credentials">
        /// [Required] Account credentials.
        /// Please note <see cref="MachineLearningDatastoreCredentials"/> is the base class. According to the scenario, a derived class of the base class might need to be assigned here, or this property needs to be casted to one of the possible derived classes.
        /// The available derived classes include <see cref="MachineLearningAccountKeyDatastoreCredentials"/>, <see cref="MachineLearningCertificateDatastoreCredentials"/>, <see cref="MachineLearningNoneDatastoreCredentials"/>, <see cref="MachineLearningSasDatastoreCredentials"/> and <see cref="MachineLearningServicePrincipalDatastoreCredentials"/>.
        /// </param>
        /// <param name="accountName"> [Required] Storage account name. </param>
        /// <param name="filesystem"> [Required] The name of the Data Lake Gen2 filesystem. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="credentials"/>, <paramref name="accountName"/> or <paramref name="filesystem"/> is null. </exception>
        public MachineLearningAzureDataLakeGen2Datastore(MachineLearningDatastoreCredentials credentials, string accountName, string filesystem) : base(credentials)
        {
            Argument.AssertNotNull(credentials, nameof(credentials));
            Argument.AssertNotNull(accountName, nameof(accountName));
            Argument.AssertNotNull(filesystem, nameof(filesystem));

            AccountName = accountName;
            Filesystem = filesystem;
            DatastoreType = DatastoreType.AzureDataLakeGen2;
        }

        /// <summary> Initializes a new instance of MachineLearningAzureDataLakeGen2Datastore. </summary>
        /// <param name="description"> The asset description text. </param>
        /// <param name="properties"> The asset property dictionary. </param>
        /// <param name="tags"> Tag dictionary. Tags can be added, removed, and updated. </param>
        /// <param name="credentials">
        /// [Required] Account credentials.
        /// Please note <see cref="MachineLearningDatastoreCredentials"/> is the base class. According to the scenario, a derived class of the base class might need to be assigned here, or this property needs to be casted to one of the possible derived classes.
        /// The available derived classes include <see cref="MachineLearningAccountKeyDatastoreCredentials"/>, <see cref="MachineLearningCertificateDatastoreCredentials"/>, <see cref="MachineLearningNoneDatastoreCredentials"/>, <see cref="MachineLearningSasDatastoreCredentials"/> and <see cref="MachineLearningServicePrincipalDatastoreCredentials"/>.
        /// </param>
        /// <param name="datastoreType"> [Required] Storage type backing the datastore. </param>
        /// <param name="isDefault"> Readonly property to indicate if datastore is the workspace default datastore. </param>
        /// <param name="accountName"> [Required] Storage account name. </param>
        /// <param name="endpoint"> Azure cloud endpoint for the storage account. </param>
        /// <param name="filesystem"> [Required] The name of the Data Lake Gen2 filesystem. </param>
        /// <param name="protocol"> Protocol used to communicate with the storage account. </param>
        /// <param name="serviceDataAccessAuthIdentity"> Indicates which identity to use to authenticate service data access to customer&apos;s storage. </param>
        internal MachineLearningAzureDataLakeGen2Datastore(string description, IDictionary<string, string> properties, IDictionary<string, string> tags, MachineLearningDatastoreCredentials credentials, DatastoreType datastoreType, bool? isDefault, string accountName, string endpoint, string filesystem, string protocol, MachineLearningServiceDataAccessAuthIdentity? serviceDataAccessAuthIdentity) : base(description, properties, tags, credentials, datastoreType, isDefault)
        {
            AccountName = accountName;
            Endpoint = endpoint;
            Filesystem = filesystem;
            Protocol = protocol;
            ServiceDataAccessAuthIdentity = serviceDataAccessAuthIdentity;
            DatastoreType = datastoreType;
        }

        /// <summary> [Required] Storage account name. </summary>
        public string AccountName { get; set; }
        /// <summary> Azure cloud endpoint for the storage account. </summary>
        public string Endpoint { get; set; }
        /// <summary> [Required] The name of the Data Lake Gen2 filesystem. </summary>
        public string Filesystem { get; set; }
        /// <summary> Protocol used to communicate with the storage account. </summary>
        public string Protocol { get; set; }
        /// <summary> Indicates which identity to use to authenticate service data access to customer&apos;s storage. </summary>
        public MachineLearningServiceDataAccessAuthIdentity? ServiceDataAccessAuthIdentity { get; set; }
    }
}

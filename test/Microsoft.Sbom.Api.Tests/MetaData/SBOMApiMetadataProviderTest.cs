﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Sbom.Common.Config;
using Microsoft.Sbom.Api.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Sbom.Contracts;
using System.Xml.Linq;

namespace Microsoft.Sbom.Api.Tests.Config
{
    [TestClass]
    public class SBOMApiMetadataProviderTest
    {
        private Configuration config;
        private SBOMMetadata metadata;

        [TestInitialize]
        public void TestInitialize()
        {
            config = new Configuration
            {
                NamespaceUriUniquePart = new ConfigurationSetting<string>("some-custom-value-here"),
                NamespaceUriBase = new ConfigurationSetting<string>("http://sbom.microsoft")
            };
            metadata = new SBOMMetadata
            {
                BuildId = "buildId",
                PackageName = "packageName",
                PackageVersion = "packageVersion",
                BuildName = "buildName",
                RepositoryUri = "repositoryUri",
                Branch = "branch",
                CommitId = "commitId"
            };
        }

        [TestMethod]
        public void SBOMApiMetadataProvider_BuildEnvironmentName_WithMetadata()
        {
            metadata.BuildEnvironmentName = "name";

            SBOMApiMetadataProvider sbomApiMetadataProvider = new SBOMApiMetadataProvider(metadata, config);
            Assert.AreEqual("name", sbomApiMetadataProvider.BuildEnvironmentName);
        }

        [TestMethod]
        public void SBOMApiMetadataProvider_BuildEnvironmentName_WithoutMetadata()
        {
            SBOMApiMetadataProvider sbomApiMetadataProvider = new SBOMApiMetadataProvider(metadata, config);
            Assert.AreEqual(null, sbomApiMetadataProvider.BuildEnvironmentName);
        }

        [TestMethod]
        public void SBOMApiMetadataProvider_GetDocumentNamespaceUri()
        {
            SBOMApiMetadataProvider sbomApiMetadataProvider = new SBOMApiMetadataProvider(metadata, config);
            Assert.AreEqual("http://sbom.microsoft/packageName/packageVersion/some-custom-value-here", sbomApiMetadataProvider.GetDocumentNamespaceUri());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SBOMApiMetadataProvider_WithNullConfiguration_ThrowArgumentNullException()
        {
            new SBOMApiMetadataProvider(metadata, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SBOMApiMetadataProvider_WithNullMetadata_ThrowArgumentNullException()
        {
            new SBOMApiMetadataProvider(null, config);
        }
    }
}
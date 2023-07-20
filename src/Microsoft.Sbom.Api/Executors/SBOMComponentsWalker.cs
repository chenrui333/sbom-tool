﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.ComponentDetection.Contracts.BcdeModels;
using Microsoft.ComponentDetection.Contracts.TypedComponent;
using Microsoft.Extensions.Logging;
using Microsoft.Sbom.Api.Utils;
using Microsoft.Sbom.Common;
using Microsoft.Sbom.Common.Config;
using Microsoft.Sbom.Extensions;

namespace Microsoft.Sbom.Api.Executors;

/// <summary>
/// Runs the component detection tool and returns a list of SBOM components scanned in the given folder.
/// </summary>
public class SBOMComponentsWalker : ComponentDetectionBaseWalker
{
    public SBOMComponentsWalker(ILogger<SBOMComponentsWalker> log, ComponentDetectorCachedExecutor componentDetector, IConfiguration configuration, ISbomConfigProvider sbomConfigs, IFileSystemUtils fileSystemUtils)
        : base(log, componentDetector, configuration, sbomConfigs, fileSystemUtils)
    {
    }

    protected override IEnumerable<ScannedComponent> FilterScannedComponents(ScanResult result)
    {
        return result
            .ComponentsFound
            .Where(component => component.Component is SpdxComponent)
            .Distinct(new ScannedComponentEqualityComparer())
            .ToList();
    }
}
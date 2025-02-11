// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Sbom.Adapters.ComponentDetection;

using Microsoft.ComponentDetection.Contracts.TypedComponent;
using Microsoft.Sbom.Contracts;

/// <summary>
/// Extensions methods for <see cref="RubyGemsComponent" />.
/// </summary>
internal static class RubyGemsComponentExtensions
{
    /// <summary>
    /// Converts a <see cref="RubyGemsComponent" /> to an <see cref="SbomPackage" />.
    /// </summary>
    /// <param name="rubyGemsComponent">The <see cref="RubyGemsComponent" /> to convert.</param>
    /// <param name="license">The license to use.</param>
    /// <returns>The converted <see cref="SbomPackage" />.</returns>
    public static SbomPackage ToSbomPackage(this RubyGemsComponent rubyGemsComponent, string? license = null) => new()
    {
        Id = rubyGemsComponent.Id,
        PackageUrl = rubyGemsComponent.PackageUrl?.ToString(),
        PackageName = rubyGemsComponent.Name,
        PackageVersion = rubyGemsComponent.Version,
        PackageSource = rubyGemsComponent.Source,
        LicenseInfo = string.IsNullOrWhiteSpace(license) ? null : new LicenseInfo
        {
            Concluded = license,
        },
        FilesAnalyzed = false,
        Type = "ruby",
    };
}

﻿using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Sbom.Config;
using Microsoft.Sbom.Entities;
using Microsoft.Sbom.Interfaces;
using Microsoft.Sbom.Spdx3_0.Core;
using Microsoft.Sbom.Utils;

namespace Microsoft.Sbom.Processors;
internal class PackagesProcessor : IProcessor
{
    private readonly Configuration configuration;
    private readonly IEnumerable<ISourceProvider> sourceProviders;
    private readonly ILogger logger;
    private readonly IdentifierUtils identifierUtils;

    public PackagesProcessor(Configuration configuration, IEnumerable<ISourceProvider> sourceProviders, ILogger logger)
    {
        this.configuration = configuration;
        this.sourceProviders = sourceProviders;
        this.logger = logger;
        identifierUtils = new IdentifierUtils(configuration);
    }

    public async Task ProcessAsync(ChannelWriter<Element> serializerChannel, ChannelWriter<ErrorInfo> errorsChannel, ChannelWriter<Uri> identifierChannel)
    {
        try
        {
            foreach (var sourceProvider in sourceProviders)
            {
                await foreach (var package in sourceProvider.Get())
                {
                    if (package is Spdx3_0.Software.Package sbomPackage)
                    {
                        await serializerChannel.WriteAsync(sbomPackage);
                        await identifierChannel.WriteAsync(identifierUtils.GetPackageId());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errorsChannel.TryWrite(new ErrorInfo(nameof(PackagesProcessor), ex, string.Empty));
        }
    }
}

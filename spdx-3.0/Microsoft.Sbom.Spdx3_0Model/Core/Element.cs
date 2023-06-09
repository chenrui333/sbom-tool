﻿using System.Text.Json.Serialization;

namespace Microsoft.Sbom.Spdx3_0.Core;

public abstract record Element(List<NamespaceMap>? namespaces,
                               ExternalMap? imports,
                               [property:JsonPropertyName("@id")]
                               [property:JsonPropertyOrder(-1)]
                               Uri? spdxId,
                               string? name,
                               string? summary,
                               string? description,
                               string? comment,
                               CreationInfo? creationInfo,
                               IList<IntegrityMethod>? verifiedUsing,
                               ExternalReference? externalReference,
                               IList<ExternalIdentifier>? externalIdentifiers)
    : Payload(creationInfo, namespaces, imports);

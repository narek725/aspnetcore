// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Routing
{
    /// <summary>
    /// Represents metadata used during link generation. If <see cref="SuppressLinkGeneration"/> is <c>true</c> 
    /// the associated endpoint will not be used for link generation.
    /// </summary>
    public interface ISuppressLinkGenerationMetadata
    {
        /// <summary>
        /// Gets a value indicating whether the associated endpoint should be used for link generation.
        /// </summary>
        bool SuppressLinkGeneration { get; }
    }
}
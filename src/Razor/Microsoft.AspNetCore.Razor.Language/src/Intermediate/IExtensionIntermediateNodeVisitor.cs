// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Razor.Language.Intermediate
{
    public interface IExtensionIntermediateNodeVisitor<TNode> where TNode : ExtensionIntermediateNode
    {
        void VisitExtension(TNode node);
    }
}

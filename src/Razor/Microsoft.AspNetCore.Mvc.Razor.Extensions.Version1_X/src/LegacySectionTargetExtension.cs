// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.Language.CodeGeneration;

namespace Microsoft.AspNetCore.Razor.Language.Extensions
{
    internal class LegacySectionTargetExtension : ISectionTargetExtension
    {
        private const string DefaultWriterName = "__razor_section_writer";

        public const string DefaultSectionMethodName = "DefineSection";

        public string SectionMethodName { get; set; } = DefaultSectionMethodName;

        public void WriteSection(CodeRenderingContext context, SectionIntermediateNode node)
        {
            context.CodeWriter
                .WriteStartMethodInvocation(SectionMethodName)
                .Write("\"")
                .Write(node.SectionName)
                .Write("\", ");

            using (context.CodeWriter.BuildAsyncLambda(DefaultWriterName))
            {
                context.RenderChildren(node);
            }

            context.CodeWriter.WriteEndMethodInvocation(endLine: true);
        }
    }
}

// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNetCore.Razor.Language.Syntax.InternalSyntax
{
    internal sealed partial class MarkupStartTagSyntax
    {
        private const string MarkupTransitionKey = "MarkupTransition";

        public bool IsMarkupTransition
        {
            get
            {
                var annotation = GetAnnotations().FirstOrDefault(n => n.Kind == MarkupTransitionKey);
                return annotation != null;
            }
        }

        public MarkupStartTagSyntax AsMarkupTransition()
        {
            var annotations = new List<SyntaxAnnotation>(GetAnnotations())
            {
                new SyntaxAnnotation(MarkupTransitionKey, new object())
            };

            var newGreen = this.WithAnnotationsGreen(annotations.ToArray());

            return newGreen;
        }
    }
}

// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Metadata.Internal
{
    public interface IClrCollectionAccessor
    {
        bool Add([NotNull] object instance, [NotNull] object value);
        void AddRange([NotNull] object instance, [NotNull] IEnumerable<object> values);
        bool Contains([NotNull] object instance, [NotNull] object value);
        void Remove([NotNull] object instance, [NotNull] object value);
        object Create([NotNull] IEnumerable<object> values);
        object GetOrCreate([NotNull] object instance);
        Type CollectionType { get; }
    }
}

// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    public static class RelationalEntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder ToTable(
            [NotNull] this EntityTypeBuilder entityTypeBuilder,
            [CanBeNull] string name)
        {
            Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));
            Check.NullButNotEmpty(name, nameof(name));

            ((IInfrastructure<InternalEntityTypeBuilder>)entityTypeBuilder).GetInfrastructure()
                .Relational(ConfigurationSource.Explicit)
                .ToTable(name);

            return entityTypeBuilder;
        }

        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(
            [NotNull] this EntityTypeBuilder<TEntity> entityTypeBuilder,
            [CanBeNull] string name)
            where TEntity : class
            => (EntityTypeBuilder<TEntity>)ToTable((EntityTypeBuilder)entityTypeBuilder, name);

        public static EntityTypeBuilder ToTable(
            [NotNull] this EntityTypeBuilder entityTypeBuilder,
            [CanBeNull] string name,
            [CanBeNull] string schema)
        {
            Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));
            Check.NullButNotEmpty(name, nameof(name));
            Check.NullButNotEmpty(schema, nameof(schema));

            ((IInfrastructure<InternalEntityTypeBuilder>)entityTypeBuilder).GetInfrastructure()
                .Relational(ConfigurationSource.Explicit)
                .ToTable(name, schema);

            return entityTypeBuilder;
        }

        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(
            [NotNull] this EntityTypeBuilder<TEntity> entityTypeBuilder,
            [CanBeNull] string name,
            [CanBeNull] string schema)
            where TEntity : class
            => (EntityTypeBuilder<TEntity>)ToTable((EntityTypeBuilder)entityTypeBuilder, name, schema);

        public static DiscriminatorBuilder HasDiscriminator([NotNull] this EntityTypeBuilder entityTypeBuilder)
        {
            Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));

            return ((IInfrastructure<InternalEntityTypeBuilder>)entityTypeBuilder).GetInfrastructure()
                .Relational(ConfigurationSource.Explicit).HasDiscriminator();
        }

        public static DiscriminatorBuilder HasDiscriminator(
            [NotNull] this EntityTypeBuilder entityTypeBuilder,
            [NotNull] string name,
            [NotNull] Type discriminatorType)
        {
            Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(discriminatorType, nameof(discriminatorType));

            return ((IInfrastructure<InternalEntityTypeBuilder>)entityTypeBuilder).GetInfrastructure()
                .Relational(ConfigurationSource.Explicit).HasDiscriminator(name, discriminatorType);
        }

        public static DiscriminatorBuilder<TDiscriminator> HasDiscriminator<TDiscriminator>(
            [NotNull] this EntityTypeBuilder entityTypeBuilder,
            [NotNull] string name)
        {
            Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));
            Check.NotEmpty(name, nameof(name));

            return new DiscriminatorBuilder<TDiscriminator>(((IInfrastructure<InternalEntityTypeBuilder>)entityTypeBuilder).GetInfrastructure()
                .Relational(ConfigurationSource.Explicit).HasDiscriminator(name, typeof(TDiscriminator)));
        }

        public static DiscriminatorBuilder<TDiscriminator> HasDiscriminator<TEntity, TDiscriminator>(
            [NotNull] this EntityTypeBuilder<TEntity> entityTypeBuilder,
            [NotNull] Expression<Func<TEntity, TDiscriminator>> propertyExpression)
            where TEntity : class
        {
            Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));
            Check.NotNull(propertyExpression, nameof(propertyExpression));

            return new DiscriminatorBuilder<TDiscriminator>(((IInfrastructure<InternalEntityTypeBuilder>)entityTypeBuilder).GetInfrastructure()
                .Relational(ConfigurationSource.Explicit).HasDiscriminator(propertyExpression.GetPropertyAccess()));
        }
    }
}

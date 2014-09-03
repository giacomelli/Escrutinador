using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HelperSharp;

namespace Escrutinador
{
    internal class DefaultProxyMetadataProvider : IMetadataProvider
    {
        private IMetadataProvider m_underlyingProvider;

        public DefaultProxyMetadataProvider(IMetadataProvider underlyingProvider)
        {
            ExceptionHelper.ThrowIfNull("underlyingProvider", underlyingProvider);

            m_underlyingProvider = underlyingProvider;
        }

        public IList<PropertyMetadata<TData>> Properties<TData>()
        {
            return m_underlyingProvider.Properties<TData>();
        }

        public PropertyMetadata<TData> Property<TData>(Expression<Func<TData, object>> property)
        {
            var propertyMetadata = m_underlyingProvider.Property(property);

            EscrutinadorConfig.Resolve<TData>(propertyMetadata);

            return propertyMetadata;
        }
    }
}

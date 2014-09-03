using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Escrutinador
{
    /// <summary>
    /// Defines an interface for a metadata provider.
    /// </summary>
    public interface IMetadataProvider
    {
        /// <summary>
        /// Gets the properties metatada of the specified data.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns>The properties metadata.</returns>
        IList<PropertyMetadata<TData>> Properties<TData>();

        /// <summary>
        /// Gets the property metadata of the specified data.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The property metatada.</returns>
        PropertyMetadata<TData> Property<TData>(Expression<Func<TData, object>> property);
    }
}

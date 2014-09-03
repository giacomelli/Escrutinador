using System.Collections.Generic;
using System.Linq;

namespace Escrutinador
{
    /// <summary>
    /// Escrutinador global configuration.
    /// </summary>
    public static class EscrutinadorConfig
    {
        #region Fields
        private static IMetadataProvider s_metadataProvider;
        private static List<IMetadataResolver> s_resolvers = new List<IMetadataResolver>();
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes static members of the <see cref="EscrutinadorConfig"/> class.
        /// </summary>
        static EscrutinadorConfig()
        {
            MetadataProvider = new DataAnnotationsMetadataProvider();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the metadata provider.
        /// </summary>
        /// <value>
        /// The metadata provider.
        /// </value>
        public static IMetadataProvider MetadataProvider
        {
            get
            {
                return s_metadataProvider;
            }
            set
            {
                s_metadataProvider = new DefaultProxyMetadataProvider(value);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a metadata resolver.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public static void AddResolver(IMetadataResolver resolver)
        {
            s_resolvers.Add(resolver);
        }

        /// <summary>
        /// Resolves the specified property metadata using the previous add metadata resolvers.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="propertyMetadata">The property metadata.</param>
        public static void Resolve<TData>(PropertyMetadata<TData> propertyMetadata)
        {
            var result = propertyMetadata;

            foreach (var resolver in s_resolvers.Where(r => r.CanResolve<TData>()))
            {
                resolver.Resolve(result);
            }
        }
        #endregion
    }
}

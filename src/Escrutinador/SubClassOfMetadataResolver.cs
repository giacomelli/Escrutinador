using System;

namespace Escrutinador
{
	/// <summary>
	/// An' sub class of' metadata resolver.
	/// </summary>
    public class SubClassOfMetadataResolver<TSubClassOf> : IMetadataResolver
    {
        private Type m_subClassOfType;
        private Action<IPropertyMetadata> m_resolve;

		/// <summary>
		/// Initializes a new instance of the <see cref="Escrutinador.SubClassOfMetadataResolver{TData}"/> class.
		/// </summary>
		/// <param name="resolve">Resolve.</param>
        public SubClassOfMetadataResolver(Action<IPropertyMetadata> resolve)
        {
            m_subClassOfType = typeof(TSubClassOf);
            m_resolve = resolve;
        }

		/// <summary>
		/// Determines whether if can revolve the data type specified.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <typeparam name="TData">The 1st type parameter.</typeparam>
        public bool CanResolve<TData>()
        {
            return typeof(TData) == m_subClassOfType;
        }

		/// <summary>
		/// Resolve the specified property metadata.
		/// </summary>
		/// <param name="propertyMetadata">Property metadata.</param>
        public virtual void Resolve(IPropertyMetadata propertyMetadata)
        {
            m_resolve(propertyMetadata);
        }
    }
}

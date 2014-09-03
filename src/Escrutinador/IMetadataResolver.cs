
namespace Escrutinador
{
	/// <summary>
	/// Defines an interface for a metadata resolver.
	/// </summary>
    public interface IMetadataResolver
    {
		/// <summary>
		/// Determines whether if can revolve the data type specified.
		/// </summary>
		/// <returns><c>true</c> if this instance can resolve; otherwise, <c>false</c>.</returns>
		/// <typeparam name="TData">The 1st type parameter.</typeparam>
        bool CanResolve<TData>();

		/// <summary>
		/// Resolve the specified property metadata.
		/// </summary>
		/// <param name="propertyMetadata">Property metadata.</param>
        void Resolve(IPropertyMetadata propertyMetadata);
    }
}

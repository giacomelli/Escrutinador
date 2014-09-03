using System;

namespace Escrutinador.Extensions.KissSpecifications
{
	/// <summary>
	/// MustComplyWithMetadataSpecification config.
	/// </summary>
    public static class MustComplyWithMetadataSpecificationConfig
    {
        // TODO: ver qual a melhor forma de fazer isso. Talvez utilizar a mesma ideia do IMetadataResolver.
		/// <summary>
		/// The is required not satisfied by function.
		/// </summary>
        public static Func<IPropertyMetadata, object, bool?> IsRequiredNotSatisfiedBy = (m, d) => null;
    }
}

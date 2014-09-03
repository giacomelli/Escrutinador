using System;

namespace Escrutinador
{
	/// <summary>
	/// Defines an interface for property metadata.
	/// </summary>
    public interface IPropertyMetadata
    {
		/// <summary>
		/// Gets the type of the data.
		/// </summary>
		Type DataType { get; }

		/// <summary>
		/// Gets or sets the maximum length.
		/// </summary>
        int MaxLength { get; set; }

		/// <summary>
		/// Gets or sets the minimum length.
		/// </summary>
        int MinLength { get; set; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
        string Name { get; }

		/// <summary>
		/// Gets or sets the order.
		/// </summary>
		/// <value>The order.</value>
        int Order { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the data is required.
		/// </summary>
		/// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        bool Required { get; set; }
    }
}

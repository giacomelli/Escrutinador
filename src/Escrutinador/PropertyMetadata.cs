using System;
using System.Diagnostics;
using System.Reflection;

namespace Escrutinador
{
	/// <summary>
	/// Represents a property metadata.
	/// </summary>
    [DebuggerDisplay("{Name} = MinLength:{MinLength},MaxLength:{MaxLength},Required:{Required}")]
    public class PropertyMetadata<TData> : IPropertyMetadata
    {
        #region Fields
        private PropertyInfo m_propertyInfo;
        #endregion

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="PropertyMetadata{TData}"/> class.
		/// </summary>
		/// <param name="propertyInfo">Property info.</param>
        public PropertyMetadata(PropertyInfo propertyInfo)
        {
            DataType = propertyInfo.PropertyType;
            Name = propertyInfo.Name;
            MinLength = 0;
            MaxLength = int.MaxValue;
            Required = DataType.IsValueType;
            Order = int.MaxValue;

            m_propertyInfo = propertyInfo;
        }
        #endregion

        #region Properties
		/// <summary>
		/// Gets the name.
		/// </summary>
        public string Name { get; private set; }

		/// <summary>
		/// Gets the type of the data.
		/// </summary>
        public Type DataType { get; private set; }

		/// <summary>
		/// Gets or sets the minimum length.
		/// </summary>
        public int MinLength { get; set; }

		/// <summary>
		/// Gets or sets the maximum length.
		/// </summary>
        public int MaxLength { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the data is required.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
        public bool Required { get; set; }

		/// <summary>
		/// Gets or sets the order.
		/// </summary>
		/// <value>The order.</value>
        public int Order { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is URL.
		/// </summary>
		/// <value><c>true</c> if this instance is URL; otherwise, <c>false</c>.</value>
        public bool IsUrl { get; set; }
        #endregion

        #region Methods
		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="dataInstance">Data instance.</param>
		/// <typeparam name="TValue">The 1st type parameter.</typeparam>
        public TValue GetValue<TValue>(TData dataInstance)
        {
            return (TValue)m_propertyInfo.GetValue(dataInstance);
        }
        #endregion
    }
}

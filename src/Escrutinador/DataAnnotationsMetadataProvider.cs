using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HelperSharp;

namespace Escrutinador
{
    /// <summary>
    /// An IMetadataProvier's implementation that read metadatas from DataAnnotations.
    /// </summary>
    public class DataAnnotationsMetadataProvider : IMetadataProvider
    {
        #region Public methods
        /// <summary>
        /// Gets the properties metatada of the specified data.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns>
        /// The properties metadata.
        /// </returns>
        public IList<PropertyMetadata<TData>> Properties<TData>()
        {
            var metadata = new List<PropertyMetadata<TData>>();
            var dataType = typeof(TData);
            var properties = dataType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var p in properties)
            {
                metadata.Add(Property<TData>(p));
            }

            return metadata.OrderBy(o => o.Order).ToList();
        }

        /// <summary>
        /// Gets the property metadata of the specified data.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>
        /// The property metatada.
        /// </returns>
        public PropertyMetadata<TData> Property<TData>(Expression<Func<TData, object>> property)
        {
            ExceptionHelper.ThrowIfNull("property", property);

            PropertyMetadata<TData> metadata = null;
            var propertyMember = ExpressionHelper.GetMemberExpression<TData>(property);

            if (propertyMember != null)
            {
                var propertyInfo = propertyMember.Member as System.Reflection.PropertyInfo;

                if (propertyInfo != null)
                {
                    metadata = Property<TData>(propertyInfo);
                }
            }

            return metadata;
        }
        #endregion

        #region Private methods
        private PropertyMetadata<TData> Property<TData>(PropertyInfo info)
        {
            ExceptionHelper.ThrowIfNull("info", info);

            PropertyMetadata<TData> metadata = new PropertyMetadata<TData>(info);

            GetLength(metadata, info);
            GetRequired(metadata, info);
            GetDisplay(metadata, info);
            GetIsUrl(metadata, info);

            return metadata;
        }

        private static void GetLength<TData>(PropertyMetadata<TData> metadata, PropertyInfo propertyInfo)
        {
            switch (metadata.DataType.Name)
            {
                case "String":
                    var stringLength = propertyInfo.GetCustomAttribute<StringLengthAttribute>(true);

                    if (stringLength != null)
                    {
                        metadata.MinLength = stringLength.MinimumLength;
                        metadata.MaxLength = stringLength.MaximumLength;
                    }

                    break;

                case "IList`1":
                    var minLength = propertyInfo.GetCustomAttribute<MinLengthAttribute>(true);

                    if (minLength != null)
                    {
                        metadata.MinLength = minLength.Length;
                    }

                    break;

                case "Int32":
                    metadata.MinLength = 0;
                    metadata.MaxLength = 10;
                    break;

                case "Int64":
                    metadata.MinLength = 0;
                    metadata.MaxLength = 19;
                    break;
            }

        }

        private static void GetRequired<TData>(PropertyMetadata<TData> metadata, PropertyInfo propertyInfo)
        {
            var required = propertyInfo.GetCustomAttribute<RequiredAttribute>(true);
            metadata.Required = required != null || metadata.MinLength > 0 || metadata.DataType.IsEnum;
        }

        private static void GetDisplay<TData>(PropertyMetadata<TData> metadata, PropertyInfo propertyInfo)
        {
            var display = propertyInfo.GetCustomAttribute<DisplayAttribute>(true);

            if (display != null)
            {
                metadata.Order = display.Order;
            }
        }

        private void GetIsUrl<TData>(PropertyMetadata<TData> metadata, PropertyInfo propertyInfo)
        {
            metadata.IsUrl = propertyInfo.GetCustomAttribute<UrlAttribute>(true) != null;
        }
        #endregion
    }
}

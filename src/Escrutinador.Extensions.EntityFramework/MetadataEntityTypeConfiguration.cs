using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq.Expressions;

namespace Escrutinador.Extensions.EntityFramework
{
	/// <summary>
	/// Allows an Entity Framework configuration auto map properties using the metadata.
	/// </summary>
    public class MetadataEntityTypeConfiguration<TEntityType> : EntityTypeConfiguration<TEntityType> where TEntityType : class
    {
        #region Fields
        private IMetadataProvider m_provider;
        #endregion

        #region Protected methods
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Escrutinador.Extensions.EntityFramework.MetadataEntityTypeConfiguration{TEntityType}"/> class.
		/// </summary>
		/// <param name="provider">Provider.</param>
        protected MetadataEntityTypeConfiguration(IMetadataProvider provider)
        {
            m_provider = provider;
        }

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Escrutinador.Extensions.EntityFramework.MetadataEntityTypeConfiguration{TEntityType}"/> class.
		/// </summary>
        protected MetadataEntityTypeConfiguration() : this(EscrutinadorConfig.MetadataProvider)
        {            
        }

		/// <summary>
		/// Maps the metadata.
		/// </summary>
        protected void MapMetadata()
        {
            var properties = m_provider.Properties<TEntityType>();
        }

		/// <summary>
		/// Maps the metadata.
		/// </summary>
		/// <param name="propertyExpression">Property expression.</param>
        protected void MapMetadata(Expression<Func<TEntityType, string>> propertyExpression)
        {
            var metadata = CreateMetadata(ExpressionTreeHelper.AddBox(propertyExpression));
            var property = Property(propertyExpression);

            MapRequired(metadata, property);
            MapMaxLength(metadata, property);
        }

		/// <summary>
		/// Maps the metadata.
		/// </summary>
		/// <param name="propertyExpression">Property expression.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
        protected void MapMetadata<T>(Expression<Func<TEntityType, T>> propertyExpression) where T : struct
        {
            var metadata = CreateMetadata(ExpressionTreeHelper.AddBox(propertyExpression));
            var property = Property(propertyExpression);

            MapRequired(metadata, property);
        }

		/// <summary>
		/// Maps the metadata.
		/// </summary>
		/// <param name="propertyExpression">Property expression.</param>
        protected void MapMetadata(Expression<Func<TEntityType, DateTime>> propertyExpression)
        {
            var metadata = CreateMetadata(ExpressionTreeHelper.AddBox(propertyExpression));
            var property = Property(propertyExpression);

            MapRequired(metadata, property);
        }

		/// <summary>
		/// Maps the metadata.
		/// </summary>
		/// <param name="propertyExpression">Property expression.</param>
        protected void MapMetadata(Expression<Func<TEntityType, DateTime?>> propertyExpression)
        {
            var metadata = CreateMetadata(ExpressionTreeHelper.AddBox(propertyExpression));
            var property = Property(propertyExpression);

            MapRequired(metadata, property);
        }

		/// <summary>
		/// Maps the metadata.
		/// </summary>
		/// <param name="propertyExpression">Property expression.</param>
        protected void MapMetadata(Expression<Func<TEntityType, bool>> propertyExpression)
        {
            var metadata = CreateMetadata(ExpressionTreeHelper.AddBox(propertyExpression));
            var property = Property(propertyExpression);

            MapRequired(metadata, property);
        }

		/// <summary>
		/// Maps the metadata.
		/// </summary>
		/// <param name="propertyExpression">Property expression.</param>
        protected void MapMetadata(Expression<Func<TEntityType, int>> propertyExpression)
        {
            var metadata = CreateMetadata(ExpressionTreeHelper.AddBox(propertyExpression));
            var property = Property(propertyExpression);

            MapRequired(metadata, property);
        }

		/// <summary>
		/// Maps the metadata.
		/// </summary>
		/// <param name="propertyExpression">Property expression.</param>
        protected void MapMetadata(Expression<Func<TEntityType, long>> propertyExpression)
        {
            var metadata = CreateMetadata(ExpressionTreeHelper.AddBox(propertyExpression));
            var property = Property(propertyExpression);

            MapRequired(metadata, property);
        }
        #endregion

        #region Private methods
        private static void MapMaxLength(PropertyMetadata<TEntityType> metadata, LengthPropertyConfiguration property)
        {
            property.HasMaxLength(metadata.MaxLength);
        }

        private static void MapRequired(PropertyMetadata<TEntityType> metadata, PrimitivePropertyConfiguration property)
        {
            if (metadata.Required)
            {
                property.IsRequired();
            }
        }

        private PropertyMetadata<TEntityType> CreateMetadata(Expression<Func<TEntityType, object>> propertyExpression)
        {
            var metadata = m_provider.Property(propertyExpression);
            return metadata;
        }
        #endregion
    }
}

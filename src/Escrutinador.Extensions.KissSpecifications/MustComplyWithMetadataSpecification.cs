using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using HelperSharp;
using KissSpecifications;

namespace Escrutinador.Extensions.KissSpecifications
{
    // TODO: move the satisfyBy to a Chains of Responsability.
    /// <summary>
    /// Must comply with metadata specification.
    /// </summary>
	public class MustComplyWithMetadataSpecification<TTarget> : SpecificationBase<TTarget>
    {
        #region Constants
		/// <summary>
		/// The required not satisfied reason.
		/// </summary>
        public const string RequiredNotSatisfiedReason = "The {0} is required.";

		/// <summary>
		/// The minimum length not satisfied reason.
		/// </summary>
        public const string MinLengthNotSatisfiedReason = "The minimum length to {0} is {1}.";

		/// <summary>
		/// The max length not satisfied reason.
		/// </summary>
        public const string MaxLengthNotSatisfiedReason = "The maximum length to {0} is {1}.";

		/// <summary>
		/// The URL not satisfied reason.
		/// </summary>
        public const string UrlNotSatisfiedReason = "The {0} is an invalid URL.";
        #endregion

		#region Methods
		/// <summary>
		/// Determines whether the target object satisfies the specification.
		/// </summary>
		/// <param name="target">The target object to be validated.</param>
		/// <returns><c>true</c> if this instance is satisfied by the specified target; otherwise, <c>false</c>.</returns>
        public override bool IsSatisfiedBy(TTarget target)
        {
            var propertiesMetadata = EscrutinadorConfig.MetadataProvider.Properties<TTarget>();

            foreach (var p in propertiesMetadata)
            {
                if (!IsStatisfiedByObject(p, target))
                {
                    return false;
                }

                switch (p.DataType.Name)
                {
                    case "String":
                        if (!IsStatisfiedByString(p, target))
                        {
                            return false;
                        }
                        break;

                    case "IList`1":
                        if (!IsSatisfiedByIList(p, target))
                        {
                            return false;
                        }

                        break;
                }
            }

            return true;
        }

        private bool IsStatisfiedByObject(PropertyMetadata<TTarget> propertyMetadata, TTarget target)
        {
            var value = propertyMetadata.GetValue<object>(target);
            var globalizationResolver = KissSpecificationsConfig.GlobalizationResolver;

            var isNotSatisfied = !propertyMetadata.DataType.IsEnum && propertyMetadata.Required && ObjectHelper.IsNullOrDefault(value);

            if (!isNotSatisfied)
            {
                var required = MustComplyWithMetadataSpecificationConfig.IsRequiredNotSatisfiedBy(propertyMetadata, value);

                if (required.HasValue)
                {
                    isNotSatisfied = required.Value;
                }
            }

            if (isNotSatisfied)
            {
                NotSatisfiedReason = globalizationResolver.GetText(RequiredNotSatisfiedReason).With(globalizationResolver.GetText(propertyMetadata.Name));
                return false;
            }

            return true;
        }

        private bool IsStatisfiedByString(PropertyMetadata<TTarget> propertyMetadata, TTarget target)
        {
            var stringValue = propertyMetadata.GetValue<string>(target);
            stringValue = String.IsNullOrEmpty(stringValue) ? String.Empty : stringValue;

            var result = IsStatisfiedyByLength(propertyMetadata, stringValue.Length);

            if (result 
                && propertyMetadata.IsUrl 
                && !String.IsNullOrEmpty(stringValue))
            {
                result = new UrlAttribute().IsValid(stringValue);

                if (!result)
                {
                    var globalizationResolver = KissSpecificationsConfig.GlobalizationResolver;

                    NotSatisfiedReason = globalizationResolver.GetText(UrlNotSatisfiedReason).With(globalizationResolver.GetText(propertyMetadata.Name));
                }
            }

            return result;
        }
			
        private bool IsSatisfiedByIList(PropertyMetadata<TTarget> propertyMetadata, TTarget target)
        {
            int lenght = 0;

            var list = propertyMetadata.GetValue<IList>(target);

            if (list != null)
            {
                lenght = list.Count;
            }

            return IsStatisfiedyByLength(propertyMetadata, lenght);
        }

        private bool IsStatisfiedyByLength(PropertyMetadata<TTarget> propertyMetadata, int length)
        {
            var globalizationResolver = KissSpecificationsConfig.GlobalizationResolver;

            if (length < propertyMetadata.MinLength)
            {
                NotSatisfiedReason = globalizationResolver.GetText(MinLengthNotSatisfiedReason).With(globalizationResolver.GetText(propertyMetadata.Name), propertyMetadata.MinLength);
                return false;
            }

            if (length > propertyMetadata.MaxLength)
            {
                NotSatisfiedReason = globalizationResolver.GetText(MaxLengthNotSatisfiedReason).With(globalizationResolver.GetText(propertyMetadata.Name), propertyMetadata.MaxLength);
                return false;
            }

            return true;
        }
		#endregion
    }
}

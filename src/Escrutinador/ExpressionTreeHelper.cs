using System;
using System.Linq.Expressions;

namespace Escrutinador
{
	/// <summary>
	/// Expression Tree helper.
	/// </summary>
    public static class ExpressionTreeHelper
    {        
		/// <summary>
		/// Adds the Expression&lt;Func&lt;TInput, object&gt;&gt; boxing.
		/// </summary>
		/// <returns>The box.</returns>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TInput">The 1st type parameter.</typeparam>
		/// <typeparam name="TOutput">The 2nd type parameter.</typeparam>
        public static Expression<Func<TInput, object>> AddBox<TInput, TOutput>
        (Expression<Func<TInput, TOutput>> expression)
        {
            // Add the boxing operation, but get a weakly typed expression
            Expression converted = Expression.Convert
                 (expression.Body, typeof(object));

            // Use Expression.Lambda to get back to strong typing
            return Expression.Lambda<Func<TInput, object>>
                 (converted, expression.Parameters);
        }
    }
}

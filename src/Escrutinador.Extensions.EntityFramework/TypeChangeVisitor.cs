﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Escrutinador.Extensions.EntityFramework
{
    /// <summary>
    /// ExpressionVisitor para trocar o tipo do parâmetro.
    /// Código orignal oriundo de http://stackoverflow.com/a/6698824/956886.
    /// </summary>
    public class TypeChangeVisitor : ExpressionVisitor
    {
        #region Fields
        private readonly Type m_from, m_to;
        private readonly Dictionary<Expression, Expression> m_substitutions;
        #endregion

        #region Constructors
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="TypeChangeVisitor"/>.
        /// </summary>
        /// <param name="from">O tipo original.</param>
        /// <param name="to">O tipo destino..</param>
        /// <param name="substitutions">As substituições.</param>
        public TypeChangeVisitor(Type from, Type to, Dictionary<Expression, Expression> substitutions)
        {
            this.m_from = from;
            this.m_to = to;
            this.m_substitutions = substitutions;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Dispatches the expression to one of the more specialized visit methods in this class.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        public override Expression Visit(Expression node)
        { 
            // General substitutions (for example, parameter swaps).
            Expression found;

            if (m_substitutions != null && node != null && m_substitutions.TryGetValue(node, out found))
            {
                return found;
            }

            return base.Visit(node);
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.MemberExpression" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        protected override Expression VisitMember(MemberExpression node)
        { 
            // if we see x.Name on the old type, substitute for new type.
            if (node.Member.DeclaringType == m_from)
            {
                return Expression.MakeMemberAccess(
                    Visit(node.Expression),
                    m_to.GetMember(node.Member.Name, node.Member.MemberType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Single());
            }

            return base.VisitMember(node);
        }
        #endregion
    }
}

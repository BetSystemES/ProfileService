using ProfileService.BusinessLogic.Models;
using System.Linq.Expressions;
using System.Reflection;

using ProfileService.BusinessLogic.Models.Enums;

namespace ProfileService.BusinessLogic.Helpers
{
    public static class OrderHelper
    {
        public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy<T>(string orderColumn, OrderDirection orderType)
        {
            Type typeQueryable = typeof(IQueryable<T>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");

            var outerExpression = Expression.Lambda(argQueryable, argQueryable);

            string[] props = orderColumn.Split('.');

            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");

            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            LambdaExpression lambda = Expression.Lambda(expr, arg);
            string methodName = orderType == OrderDirection.Ascending ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), type }, outerExpression.Body,
                    Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);

            return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
        }
    }
}
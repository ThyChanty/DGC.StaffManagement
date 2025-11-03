using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace DGC.StaffManagement.Shared.Extensions
{
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public static class SortExtension<T> where T : class
    {

        private static readonly string[] OrderDirections = {"asc", "desc"};
        
        public static Func<IQueryable<T>, IOrderedQueryable<T>>? GetOrderBy(string? orderColumn, string? orderDirection)
        {
            if (string.IsNullOrEmpty(orderColumn))
            {
                return null;
            }

            if (string.IsNullOrEmpty(orderDirection) || !OrderDirections.Contains(orderDirection))
            {
                orderDirection = "asc";
            }
            
            var typeQueryable = typeof(IQueryable<T>);
            var argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            var props = orderColumn.Split('.');
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            
            foreach(var prop in props)
            {
                var propertyInfo = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // No property found
                if (propertyInfo == null)
                {
                    return null;
                }
                
                expr = Expression.Property(expr, propertyInfo);
                type = propertyInfo.PropertyType;
            }
            
            var lambda = Expression.Lambda(expr, arg);
            var methodName = orderDirection == "asc" ? "OrderBy" : "OrderByDescending";
            var resultExp = Expression.Call(
                typeof(Queryable),
                methodName, 
                new [] { typeof(T), type }, 
                outerExpression.Body,
                Expression.Quote(lambda));

            return (Func<IQueryable<T>, IOrderedQueryable<T>>) Expression.Lambda(resultExp, argQueryable).Compile();
        }
    }
}
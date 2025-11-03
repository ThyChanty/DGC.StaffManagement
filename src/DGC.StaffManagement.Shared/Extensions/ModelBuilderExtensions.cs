using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DGC.StaffManagement.Shared.Extensions;

public static class ModelBuilderExtensions
{
    
    /// <summary>
    /// https://stackoverflow.com/questions/29261734/add-filter-to-all-query-entity-framework
    /// Overwrite the previous query filters on the same entity type, if exists.
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="expression"></param>
    /// <typeparam name="TInterface"></typeparam>
    public static void ApplyQueryFilter<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
    {
        var entities = modelBuilder.Model
            .GetEntityTypes()
            .Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null && e.BaseType == null)
            .Select(e => e.ClrType);
        
        foreach (var entity in entities)
        {
            var newParam = Expression.Parameter(entity);
            var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);    
            modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newBody, newParam));
        }
    }
    
    /// <summary>
    /// https://github.com/dotnet/efcore/issues/10275
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    public static void AppendQueryFilter<T>(this ModelBuilder modelBuilder, Expression<Func<T, bool>> expression)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(T).IsAssignableFrom(entityType.ClrType) || entityType.BaseType != null)
            {
                continue;
            }

            var parameterType = Expression.Parameter(entityType.ClrType);
            var expressionFilter = ReplacingExpressionVisitor.Replace(
                expression.Parameters.Single(), parameterType, expression.Body);

            var currentQueryFilter = entityType.GetQueryFilter();
            if (currentQueryFilter != null)
            {
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                    currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }

            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityType.SetQueryFilter(lambdaExpression);
        }
    }
    
    /// <summary>
    /// ref: https://stackoverflow.com/questions/46526230/disable-cascade-delete-on-ef-core-2-globally
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void DisabledCascadeDeletion(this ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        
        foreach (var fk in cascadeFKs)
        {
            fk.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }
}
using System.Linq.Expressions;

namespace Core.Entities.Specifications;


public interface ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, Object>>> Includes { get; } 
}

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {

    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, Object>>> Includes { get; } =
        new List<Expression<Func<T, object>>>();

    public void AddInclude(Expression<Func<T, Object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}
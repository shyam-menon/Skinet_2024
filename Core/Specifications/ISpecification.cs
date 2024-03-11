using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        //an expression that takes a function which takes a type that it is returning
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
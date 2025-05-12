using System.Linq.Expressions;

namespace VGameStore.Core.Specifications
{
	public class BaseSpecification<T>
	{
		public Expression<Func<T, bool>> Criteria { get; private set; }
		public List<Expression<Func<T, object>>> Includes { get; } = new();
		public List<string> IncludeStrings { get; } = new(); // For string-based Includes
		public List<IncludePath<T, object, object>> NestedIncludes { get; } = new();

		public BaseSpecification() { }

		public BaseSpecification(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}

		public void AddInclude(Expression<Func<T, object>> include)
		{
			Includes.Add(include);
		}
		public void AddInclude(string includeString) // For string-based Includes
		{
			IncludeStrings.Add(includeString);
		}
		public void AddNestedInclude<TProperty, TThenProperty>(
			Expression<Func<T, TProperty>> include,
			Expression<Func<TProperty, TThenProperty>> thenInclude)
		{
			NestedIncludes.Add(new IncludePath<T, object, object>(
				Expression.Lambda<Func<T, object>>(Expression.Convert(include.Body, typeof(object)), include.Parameters),
				Expression.Lambda<Func<object, object>>(Expression.Convert(thenInclude.Body, typeof(object)), thenInclude.Parameters)
			));
		}
	}

	public class IncludePath<T, TProperty, TThenProperty>
	{
		public Expression<Func<T, TProperty>> Include { get; set; }
		public Expression<Func<TProperty, TThenProperty>> ThenInclude { get; set; }

		public IncludePath(Expression<Func<T, TProperty>> include, Expression<Func<TProperty, TThenProperty>> thenInclude)
		{
			Include = include;
			ThenInclude = thenInclude;
		}
	}
}
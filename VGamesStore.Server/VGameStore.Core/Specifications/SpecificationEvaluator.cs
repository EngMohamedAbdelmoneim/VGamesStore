﻿using Microsoft.EntityFrameworkCore;

namespace VGameStore.Core.Specifications
{
	public class SpecificationEvaluator<T> where T : class
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, BaseSpecification<T> spec)
		{
			var query = inputQuery;

			if (spec.Criteria != null)
				query = query.Where(spec.Criteria);

			query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
			query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

			return query;
		}
	}

}
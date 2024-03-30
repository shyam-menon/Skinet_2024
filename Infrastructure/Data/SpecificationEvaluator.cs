using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // Class responsible for evaluating specifications
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        // Method to get the query based on the specification
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            // Apply criteria if specified
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // Apply ordering if specified
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            // Apply paging if specified
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // Apply includes
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

        
    }
}


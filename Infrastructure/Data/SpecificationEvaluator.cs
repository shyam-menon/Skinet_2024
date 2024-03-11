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

            // Apply includes
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

        
    }
}


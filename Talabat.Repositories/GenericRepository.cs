﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Repositories.Data;

namespace Talabat.Repositories
{
    public class GenericRepository<T> : IRepositories<T> where T : ModelBase
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Product))
            //    return (IEnumerable<T>)await _context.Set<Product>().Include(B => B.ProductBrand).Include(C => C.productCategory).ToListAsync();

            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }



        public async Task<T?> GetByIdWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();

        }

        public IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvaluator.GetQuery<T>(_context.Set<T>(), Spec);
        }

        public Task<int> GetCount(Expression<Func<T, bool>> CountExperssion)
        {
            return _context.Set<T>().CountAsync(CountExperssion);
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).CountAsync();

        }

        public void Add(T item)
         => _context.AddAsync(item);


        public void Update(T item)
        => _context.Update(item);

        public void Delete(T item)
        => _context.Remove(item);
    }
}

using Microsoft.EntityFrameworkCore;
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
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>)await _context.Set<Product>().Include(B => B.ProductBrand).Include(C => C.productCategory).ToListAsync();

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}

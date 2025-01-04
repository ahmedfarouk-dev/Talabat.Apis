using System.Collections;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Repositories;
using Talabat.Repositories.Data;

namespace Talabat.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;

        private readonly Hashtable _Repository;
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _Repository = new Hashtable();
        }

        public IRepositories<T> Repository<T>() where T : ModelBase
        {
            var Key = typeof(T).Name;

            if (!_Repository.ContainsKey(Key))
            {
                var Result = new GenericRepository<T>(_dbContext);
                _Repository.Add(Key, Result);
            }
            return _Repository[Key] as IRepositories<T>;
        }

        public void Dispose()
        => _dbContext.Dispose();

        public async Task<int> Complete()
       => await _dbContext.SaveChangesAsync();
    }
}

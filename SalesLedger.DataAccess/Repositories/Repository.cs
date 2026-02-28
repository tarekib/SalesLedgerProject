using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SalesLedger.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SalesLedgerDbContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(SalesLedgerDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public T GetById(int id) => DbSet.Find(id);

        public IEnumerable<T> GetAll() => DbSet.ToList();

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
            => DbSet.Where(predicate).ToList();

        public void Add(T entity) => DbSet.Add(entity);

        public void Remove(T entity) => DbSet.Remove(entity);
    }
}

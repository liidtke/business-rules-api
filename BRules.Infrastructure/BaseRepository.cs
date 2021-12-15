
using BRules.Domain.SharedKernel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BRules.Infrastructure
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {
        protected readonly IMongoCollection<TEntity> DbSet;
        protected readonly MongoContext Context;

        public BaseRepository(MongoContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task Add(TEntity obj)
        {
            await DbSet.InsertOneAsync(obj);
        }

        public IQueryable<TEntity> Query()
        {
            return DbSet.AsQueryable();
        }

        public virtual Task<List<TEntity>> Get()
        {
            return DbSet.Find(e => true).ToListAsync();
        }

        public virtual Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Find(filter).ToListAsync();
        }

        public virtual async Task<TEntity> Find(string id)
        {
            var data = await DbSet.FindAsync(x => x.Id == id);
            return data.FirstOrDefault();
        }

        public Task Remove(string id)
        {
            DbSet.DeleteOne(e => e.Id == id);
            return Task.CompletedTask;
        }

        public async Task Update(TEntity obj)
        {
            await DbSet.ReplaceOneAsync(e => e.Id == obj.Id.ToString(), obj);
        }

        public async Task<List<TEntity>> Get(IList<string> ids)
        {
            return await DbSet.Find(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<bool> Exists(string id)
        {
            var count = await DbSet.CountDocumentsAsync(x => x.Id == id);
            return count > 0;
        }

    }
}

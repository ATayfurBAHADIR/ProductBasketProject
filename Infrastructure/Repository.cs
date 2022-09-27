using Domain.Interfaces;
using Infrastructure.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<T> DbSet;

        public Repository(IMongoContext context)
        {
            Context = context;
            DbSet = Context.GetCollection<T>(typeof(T).Name);
        }
        public async virtual Task Add(T entity)
        {

            await DbSet.InsertOneAsync(entity);
        }

        public virtual async Task<T> GetById(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<T>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual IEnumerable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Find<T>(predicate).ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var all = await DbSet.FindAsync(Builders<T>.Filter.Empty);
            return all.ToList();
        }

        public virtual async Task Update(T entity)
        {
            await DbSet.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", entity.GetId()), entity);
        }

        public virtual async Task Remove(Guid id)
        {
            await DbSet.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}

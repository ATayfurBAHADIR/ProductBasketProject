using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task Add(T entity); 
        Task<T> GetById(Guid Id);
        IEnumerable<T> Filter(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task Update(T entity);
        Task Remove(Guid Id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRules.Domain.SharedKernel
{
    public interface IRepository<TEntity> where TEntity : DomainEntity
    {
        Task<List<TEntity>> Get();
        Task<List<TEntity>> Get(IList<string> ids);
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter);
        Task Add(TEntity obj);
        Task Update(TEntity obj);
        Task Remove(string id);
        Task<TEntity> Find(string id);
        Task<bool> Exists(string id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Application.Contract
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> Createasync(TEntity entity);
        Task<TEntity> Updateasync(TEntity entity);
        Task<TEntity> Deleteasync(TEntity entity);
        Task<TEntity> GetOneasync(TId Id);
        Task<IQueryable<TEntity>> GetAllasync();
        Task<int> SaveAsync();
    }
}

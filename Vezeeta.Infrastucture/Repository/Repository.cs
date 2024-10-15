using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract;
using Vezeeta.Context;

namespace Vezeeta.Infrastucture.Repository
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly VezeetaContext _VezeetaContext;
        private readonly DbSet<TEntity> _Dbset;

        public Repository(VezeetaContext vezeetaContext)
        {
            _VezeetaContext = vezeetaContext;
            _Dbset = _VezeetaContext.Set<TEntity>();
        }
        public async Task<TEntity> Createasync(TEntity entity)
        {
            return (await _Dbset.AddAsync(entity)).Entity;
        }

        public Task<TEntity> Deleteasync(TEntity entity)
        {
            return Task.FromResult(_Dbset.Remove(entity).Entity);
        }

        public Task<IQueryable<TEntity>> GetAllasync()
        {
            return Task.FromResult(_Dbset.Select(s => s));
        }

        public async Task<TEntity> GetOneasync(TId Id)
        {
            return (await _Dbset.FindAsync(Id));
        }

        public async Task<int> SaveAsync()
        {
            return await _VezeetaContext.SaveChangesAsync();
        }

        public Task<TEntity> Updateasync(TEntity entity)
        {
            return Task.FromResult(_Dbset.Update(entity).Entity);
        }
    }
}

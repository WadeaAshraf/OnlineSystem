using E_CommerceCore;
using E_CommerceInfrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceInfrastructure
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private E_CommerceDbContext _CommerceDbContext;
        public Repository(E_CommerceDbContext e_CommerceDbContext) { 
            _CommerceDbContext = e_CommerceDbContext;
        }

        public async Task<T> AddAsyn(T t)
        {
           await _CommerceDbContext.Set<T>().AddAsync(t);
            return t;
        }

        public int Delete(T t)
        {
           _CommerceDbContext.Set<T>().Remove(t);
           return 0;
        }

        public async Task<List<T>> GetAllAsyn()
        {
           return await _CommerceDbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAsyn(Expression<Func<T, bool>> Match)
        {
            return await _CommerceDbContext.Set<T>().Where(Match).ToListAsync();
        }

        public async Task SaveChangesAsyn()
        {
             await _CommerceDbContext.SaveChangesAsync();
        }

        public T Update(T t)
        {
              _CommerceDbContext.Set<T>().Update(t) ;
            return t;
        }
        public async Task<int> GetCountAsyn()
        {
            return await _CommerceDbContext.Set<T>().CountAsync();
        }
    }
}

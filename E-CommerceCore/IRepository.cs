using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceCore
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsyn(T t);      
        int Delete(T entity);
        Task<List<T>> GetAsyn(Expression<Func<T,bool>>Match);
        Task<List<T>> GetAllAsyn();
        T Update(T t);
        Task SaveChangesAsyn();
        Task<int> GetCountAsyn();
    }
}

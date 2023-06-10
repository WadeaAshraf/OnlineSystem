using E_CommerceCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceCore.IRepositories
{
    public interface IProductsRepository:IRepository<Products>
    {
        public Task<List<IGrouping<Categories, Products>>> GetProductsWithPagenation(int PageIndex, int PageSize);
    }
}

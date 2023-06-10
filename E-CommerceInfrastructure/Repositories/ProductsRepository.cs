using E_CommerceCore.Entities;
using E_CommerceCore.IRepositories;
using E_CommerceInfrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceInfrastructure.Repositories
{
    public class ProductsRepository : Repository<Products>, IProductsRepository
    {
        private readonly E_CommerceDbContext oE_CommerceDbContext;
        public ProductsRepository(E_CommerceDbContext e_CommerceDbContext) : base(e_CommerceDbContext)
        {
            oE_CommerceDbContext = e_CommerceDbContext;
        }

        public async Task<List<IGrouping<Categories,Products>>> GetProductsWithPagenation(int PageIndex, int PageSize)
        {
            var query = await oE_CommerceDbContext.Products.Include(x=>x.Categories)
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .OrderBy(x => x.Name)
            .GroupBy(x=>x.Categories)
            //.Select(g => new
            //{
            //    CategoryName=g.Key.Name,

            //})           
            .ToListAsync();
            return query;
        }
    }
}

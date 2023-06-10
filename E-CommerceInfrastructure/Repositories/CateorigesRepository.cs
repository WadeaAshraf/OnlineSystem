using E_CommerceCore.Entities;
using E_CommerceCore.IRepositories;
using E_CommerceInfrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceInfrastructure.Repositories
{
    public class CateorigesRepository : Repository<Categories>,ICategoriesRepository
    {
        public CateorigesRepository(E_CommerceDbContext e_CommerceDbContext) : base(e_CommerceDbContext)
            {
            }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class CategoryProductDTO
    {
        public string CategoryName { get; set; }
        public List<ProductsDTO> Products { get; set; }
    }
}

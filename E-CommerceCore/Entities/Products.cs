using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceCore.Entities
{
    public class Products
    {
        public int Id { get; set; }
        [ForeignKey("Categories")]
        public int? CategoryId { get; set; }
        public string Name{ get; set; }
        public string ArName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        public bool HasAvailableStock { get; set; }
        public Categories Categories { get; set; }
       
    }
}

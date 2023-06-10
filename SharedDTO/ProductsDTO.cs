using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class ProductsDTO
    {
        public int Id { get; set; }
        
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name on Arabic is required")]
        [DisplayName("Arabic Name")]
        public string ArName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public string Price { get; set; }
        [Display(Name = "Available Stock")]
        public bool HasAvailableStock { get; set; }

        public string CategoryName { get; set; }


    }
}

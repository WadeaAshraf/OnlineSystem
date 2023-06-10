using SharedDTO;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_CommercePortal.Models
{
    public class ProductsWithImg:ProductsDTO
    {
        [Display(Name = "Image File")]
        [Required(ErrorMessage = "Image file is required")]
        public IFormFile ImageFile { get; set; }
    }
}

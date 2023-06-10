using E_CommercePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SharedDTO;

namespace E_CommercePortal.Pages.Products
{
    [MultipleRoleAuthorization(Roles = new string[] { "admin" })]
    public class AddProductModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ISettings Settings { get; set; }
        private CallingAPI CallingAPI { get; set; }
        public AddProductModel(ISettings settings, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            var sobj = httpContextAccessor.HttpContext.Session.GetString("SessionObject");
            var Token = (JsonConvert.DeserializeObject<SessionObject>(sobj))?.Token;
            Settings = settings;
            CallingAPI = new CallingAPI(settings.BaseApiUrl, Token);
            _webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public List<CategoriesDTO> LstCategories { get; set; }
        [BindProperty]
        public ProductsWithImg oProductsDTO { get; set; }
        public async Task OnGetAsync()
        {
            oProductsDTO = new ProductsWithImg();
            LstCategories = await CallingAPI.GetDataFromAPIAsync<CategoriesDTO>("Categories/GetCategoriesQuery");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save the uploaded image file
            if (oProductsDTO.ImageFile != null && oProductsDTO.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + oProductsDTO.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await oProductsDTO.ImageFile.CopyToAsync(stream);
                }

                oProductsDTO.Image = filePath;
                oProductsDTO.ImageFile = null;
            }
            var result = await CallingAPI.PostAsync<ProductsDTO, APIResult>("Products/AddNewProduct", oProductsDTO);
            if (result.IsSuccess)
            {
                return RedirectToPage("./DisplayProducts");
            }
            // Perform the necessary actions with the form data
            // For example, save the product information to a database

            return Page();
        }
    }
  
}

using E_CommercePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SharedDTO;

namespace E_CommercePortal.Pages.Products
{
    [MultipleRoleAuthorization(Roles = new string[] { "admin", "user" })]
    public class DisplayProductsModel : PageModel
    {
        [BindProperty]
       public ProductsWithPages productsDTOs { get; set; }
      
        private ISettings Settings { get; set; }
        private CallingAPI CallingAPI { get; set; }
        public DisplayProductsModel(ISettings settings, IHttpContextAccessor httpContextAccessor)
        {
            var sobj = httpContextAccessor.HttpContext.Session.GetString("SessionObject");
            var Token = (JsonConvert.DeserializeObject<SessionObject>(sobj))?.Token;
            Settings = settings;
            CallingAPI = new CallingAPI(settings.BaseApiUrl,Token);
           
        }
        public async Task OnGetAsync()
        {
            productsDTOs = await GetProducts(1,5);
        }
        public async Task<ProductsWithPages>GetProducts(int PageIndex, int PageSize=5)
        {
            var result = await CallingAPI.GetObjectFromAPIAsync<ProductsWithPages>("Products/GetProducts?PageIndex=" + PageIndex + "&PageSize=" + PageSize);
           return result;
        }
        public async Task<ActionResult>OnGetShowNextOrPreviousPage(int PageIndex, int PageSize=5)
        {
            var result = await GetProducts(PageIndex, PageSize);
            return new JsonResult(result);
        }
    }
}

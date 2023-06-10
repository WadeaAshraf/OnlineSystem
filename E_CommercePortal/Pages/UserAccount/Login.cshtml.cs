using E_CommercePortal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SharedDTO;
using System.IdentityModel.Tokens.Jwt;

namespace E_CommercePortal.Pages.UserAccount
{
    public class LoginModel : PageModel
    {
        private ISettings Settings { get; set; }
        private CallingAPI CallingAPI { get; set; }
        public LoginModel(ISettings settings)
        {
            Settings = settings;
            CallingAPI = new CallingAPI(settings.BaseApiUrl);
            
        }
        public void OnGet()
        {
        }
        [BindProperty]
       public User user { get; set; }

        public async Task<IActionResult> OnPost()
        {
           var token=await CallingAPI.PostAsync<User, string>("/Security/CreateToken",user);
            // Validate username and password

            // Encrypt the username


            // Set session value
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
            SessionObject sobj= new SessionObject();
            sobj.Token = token;
            
            // Access the payload data
            var payload = jwtToken.Payload;

            // Example: Accessing specific claims from the payload
            string role = payload["role"]?.ToString();
            sobj.Role = role;
            HttpContext.Session.SetString("SessionObject", JsonConvert.SerializeObject(sobj));
            if(role=="admin")
            {
                return RedirectToPage("../Products/AddProduct/");
            }
            else
            {
                return RedirectToPage("../Products/DisplayProducts/");
            }
        
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Invalid username or password");
            //    return Page();
            //}
        }


    }
}

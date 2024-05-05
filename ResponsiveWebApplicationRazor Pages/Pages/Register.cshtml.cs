using AuthAccess.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ResponsiveWebApplicationRazor_Pages.Pages
{
    public class RegisterModel : PageModel
    {
        public readonly IAuthApi _authApi;

        [BindProperty]
        public RegisterRequestModel RegisterRequestModel { get; set; }


        [BindProperty]
        public string ConfirmPassword { get; set; }
        public RegisterModel(IAuthApi authApi)
        {
            _authApi = authApi;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (RegisterRequestModel.Password != ConfirmPassword)
            {
                
                ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                return Page(); 
            }

            var result = await _authApi.RegisterAsync(RegisterRequestModel);

            if(result == 0)
            {

                return RedirectToPage("/Login");

            }
            else
            {
                return Page();
            }

        }
        public void OnGet()
        {
        }
    }
}

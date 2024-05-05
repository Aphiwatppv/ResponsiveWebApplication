using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ResponsiveWebApplicationRazor_Pages.Pages
{
    public class LoginModel : PageModel
    {

        private readonly IAuthApi _authApi;

        [BindProperty]
        public LoginRequestModel loginRequestModel { get; set; }

        public  LoginModel(IAuthApi authApi )
        {
            _authApi = authApi;
            CsvLogger.CsvLogger.LogInformation($"start");
  
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var sessionModel = await _authApi.LoginAsync(loginRequestModel);
                CsvLogger.CsvLogger.LogInformation($"{loginRequestModel.UserName}");
                // If sessionModel is null or doesn't meet your criteria, handle as login failure
                if (sessionModel == null)
                {
                    ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
                    return Page();
                }

                // Handle sessionModel (e.g., creating an authentication cookie)

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // Log the exception details and return an error message to the user
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }
        public void OnGet()
        {
        }
    }
}

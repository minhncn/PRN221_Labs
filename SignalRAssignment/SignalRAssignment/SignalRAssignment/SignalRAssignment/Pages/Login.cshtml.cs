using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SignalRAssignment.Controllers;
using SignalRAssignment.Domain.DTO;

namespace SignalRAssignment.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AccountController _accountController;

        [BindProperty]
        public string UserName { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";
        public string ErrorMessage { get; set; }

        public LoginModel(AccountController accountController)
        {
            _accountController = accountController;
        }

        public IActionResult OnGet(string action)
        {
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "logout":
                        HttpContext.Session.Remove("loginUser");
                        return RedirectToPage("/Index");
                    default: break;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Username and Password must not be empty. Please try again.";
                    return Page();
                }
                var loginAccount = await _accountController.LoginByUsernameAndPassword(UserName, Password);
                if  (loginAccount == null)
                {
                    ErrorMessage = "Invalid username or password. Please try again.";
                    return Page();
                }
                var loginUser = new LoginUser
                {
                    AccountId = loginAccount.AccountId,
                    UserName = loginAccount.UserName,
                    FullName = loginAccount.FullName,
                    Type = loginAccount.Type,
                };

                var loginUserJson = JsonConvert.SerializeObject(loginUser);
                HttpContext.Session.SetString("loginUser", loginUserJson);

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }
        }
    }
}

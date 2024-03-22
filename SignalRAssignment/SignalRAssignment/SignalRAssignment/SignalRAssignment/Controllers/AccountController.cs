using SignalRAssignment.Domain.Models;
using SignalRAssignment.Services.Interfaces;

namespace SignalRAssignment.Controllers
{
	public class AccountController : BaseController<AccountController>
	{
		private readonly IAccountService _accountService;

		public AccountController(ILogger<AccountController> logger, IAccountService accountService) : base(logger)
		{
			_accountService = accountService;
		}

		public async Task<Account> LoginByUsernameAndPassword(string userName, string password)
		{
			var loginAccount = await _accountService.LoginByUsernameAndPassword(userName, password);
			if (loginAccount == null)
			{
				ViewBag.ErrorMessage = "Log in failed.";
				return null;
            }
            return loginAccount;
        }
    }
}

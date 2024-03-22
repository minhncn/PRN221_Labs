using Microsoft.AspNetCore.Mvc;

namespace SignalRAssignment.Controllers
{
	public class BaseController<T> : Controller
    {
		protected ILogger<T> _logger;

		public BaseController(ILogger<T> logger)
		{
			_logger = logger;
		}
	}
}

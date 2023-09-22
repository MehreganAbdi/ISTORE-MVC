using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class SneakerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}

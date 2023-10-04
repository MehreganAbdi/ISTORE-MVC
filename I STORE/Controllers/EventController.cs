using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class EventController : Controller
    {
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}

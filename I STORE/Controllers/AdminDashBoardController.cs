using I_STORE.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class AdminDashBoardController : Controller
    {
        private readonly IAdminDashBoardService _adminDashBoardService;
        public AdminDashBoardController(IAdminDashBoardService adminDashBoardService)
        {
            _adminDashBoardService = adminDashBoardService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _adminDashBoardService.GetAllPurchases();
            return View(model);
        }

        public async Task<IActionResult> Accept(int Id)
        {
            var purchase = _adminDashBoardService.GetByIdAsync(Id);
            _adminDashBoardService.AcceptPurchase(await purchase);
            return RedirectToAction("Index", "AdminDashBoard");
        }
    }
}

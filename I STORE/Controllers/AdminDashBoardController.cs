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

        //public async Task<IActionResult> Status(int Id)
        //{

        //}
    }
}

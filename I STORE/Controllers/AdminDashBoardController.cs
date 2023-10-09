using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Application.DTOs;

namespace I_STORE.Controllers
{
    public class AdminDashBoardController : Controller
    {
        private readonly IAdminDashBoardService _adminDashBoardService;
        private readonly IEmailService _emailService;
        public AdminDashBoardController(IAdminDashBoardService adminDashBoardService, IEmailService emailService)
        {
            _adminDashBoardService = adminDashBoardService;
            _emailService = emailService;

        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("user"))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = await _adminDashBoardService.GetAllPurchases();
            return View(model);
        }

        public async Task<IActionResult> Accept(int Id)
        {
            if(!User.Identity.IsAuthenticated || User.IsInRole("user"))
            {
                return RedirectToAction("Index", "Home");
            }
            var purchase = await _adminDashBoardService.GetByIdAsync(Id);
            _adminDashBoardService.AcceptPurchase(purchase);
            var user = await _adminDashBoardService.GetUserByIdAsync(purchase.AppUserId);
            var emailInfo = new EmailDTO()
            {
                reciever = user.Email,
                subject = "Purchase Accepted",
                message = $"Your Purchase By Id : {purchase.PurchaseId} Has Been Acepted , Try To Complete Your Purchase Before We Run Out Of This Item."
            };
            await _emailService.SendEmail(emailInfo);
            return RedirectToAction("Index", "AdminDashBoard");
        }
        public async Task<IActionResult> Reject(int Id)
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("user"))
            {
                return RedirectToAction("Index", "Home");
            }
            var purchase = _adminDashBoardService.GetByIdAsync(Id);
            _adminDashBoardService.RejectPurchase(await purchase);

            return RedirectToAction("Index", "AdminDashBoard");

        }
        
    }
}

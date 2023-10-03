using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

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
            var purchase = await _adminDashBoardService.GetByIdAsync(Id);
            _adminDashBoardService.AcceptPurchase(purchase);
            var user = await _adminDashBoardService.GetUserByIdAsync(purchase.AppUserId);
            SendEmail(user.Email, "Purchase Accepted", $"Your Purchase By Id : {purchase.PurchaseId} Has Been Acepted , Try To Complete Your Purchase Before We Run Out Of This Item.");
            return RedirectToAction("Index", "AdminDashBoard");
        }
        public async Task<IActionResult> Reject(int Id)
        {
            var purchase = _adminDashBoardService.GetByIdAsync(Id);
            _adminDashBoardService.RejectPurchase(await purchase);

            return RedirectToAction("Index", "AdminDashBoard");

        }
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("mehreganabdiwebmail@gmail.com");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "kxboeipinFpkyngvgfo";
                    var sub = subject;
                    var body = message + $"\n{DateTime.Now}\nThanks For Contacting , Good Luck .";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
    }
}

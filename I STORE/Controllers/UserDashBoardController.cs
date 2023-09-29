using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Context.ViewModels;
using Context.Models;
using System.Runtime.CompilerServices;
using Context.Data.Enum;

namespace I_STORE.Controllers
{
    public class UserDashBoardController : Controller
    {
        private readonly IUserDashBoardService _userDashBoardService;
        public UserDashBoardController(IUserDashBoardService userDashBoardService)
        {
            _userDashBoardService = userDashBoardService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var model = await _userDashBoardService.GetPurchasesWithDetailsByUserIdAsync(userId);
            var tot = _userDashBoardService.ClaculateTotal(userId);

            return View(model);

        }

        public async Task<IActionResult> PurchaseProduct(int Id)
        {
            var userId = User.Identity.GetUserId();
            var purchase = new Purchase()
            {
                AppUserId = userId,
                ProductId = Id
            };

            _userDashBoardService.AddPurchase(purchase);
            return RedirectToAction("Index", "UserDashBoard");
        }
        public async Task<IActionResult> PurchaseSneaker(int Id)
        {
            var userId = User.Identity.GetUserId();
            var purchase = new Purchase()
            {
                AppUserId = userId,
                SneakerId = Id
            };
            _userDashBoardService.AddPurchase(purchase);
            return RedirectToAction("Index", "UserDashBoard");
        }


        public async Task<IActionResult> EditProfile()
        {
            var UserId = User.Identity.GetUserId();
            var user = await _userDashBoardService.GetUserByIdNoTracking(UserId);
            var VModel = new EditProfVM()
            {
                UserName = user.UserName == null ? "null" : user.UserName,
                PhoneNumber = user.PhoneNumber == null ? "0" : user.PhoneNumber,
                Address = user.Address.FullAddress
            };
            return View(VModel);
        }



        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfVM editProfVM)
        {
            var UserId = User.Identity.GetUserId();
            var user = await _userDashBoardService.GetUserByIdNoTracking(UserId);

            user.PhoneNumber = editProfVM.PhoneNumber;
            user.UserName = editProfVM.UserName;
            user.Address.FullAddress = editProfVM.Address;
            user.PhoneNumberConfirmed = true;
            var x = _userDashBoardService.UpdateUser(user);
            if (x)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(editProfVM);
        }

        public async Task<IActionResult> CancelPurchase(int Id)//Purchase Id
        {
            var purchase = await _userDashBoardService.GetPurchaseById(Id);
            if (purchase == null)
            {
                return RedirectToAction("Index", "UserDashBoard");
            }

            if (purchase.Status == Status.Done)
            {
                
                if (purchase.SneakerId != null)//Sneaker Cancelation
                {
                    _userDashBoardService.CancelSneaker((int)purchase.SneakerId);
                    _userDashBoardService.RemovePurchase(purchase);
                    return RedirectToAction("Index", "UserDashBoard");
                }

                else//Product Cancelation
                {
                    _userDashBoardService.CancelProduct((int)purchase.ProductId);
                    _userDashBoardService.RemovePurchase(purchase);
                    return RedirectToAction("Index", "UserDashBoard");
                }

            }
            _userDashBoardService.RemovePurchase(purchase);
            return RedirectToAction("Index", "UserDashBoard");
        }
    }
}

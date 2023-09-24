﻿using I_STORE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using I_STORE.ViewModels;
using I_STORE.Models;

namespace I_STORE.Controllers
{
    public class UserDashBoardController : Controller
    {
        private readonly IUserDashBoardService _userDashBoardService;
        public UserDashBoardController(IUserDashBoardService userDashBoardService)
        {
            _userDashBoardService = userDashBoardService;
        }
        //public async Task<IActionResult> Index()
        //{

        //}
        public async Task<IActionResult> PurchaseProduct(int productId)
        {
            var userId = User.Identity.GetUserId();
            var purchase = new Purchase()
            {
                AppUserId = userId,
                ProductId = productId
            };

            _userDashBoardService.AddPurchase(purchase);
            return RedirectToAction("Index", "UpperBody"); 
        } 
        public async Task<IActionResult> PurchaseSneaker(int sneakerId)
        {
            var userId = User.Identity.GetUserId();
            var purchase = new Purchase()
            {
                AppUserId = userId,
                SneakerId = sneakerId
            };
            return RedirectToAction("Index","Sneaker");
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
            user.PhoneNumberConfirmed = true;
            var x = _userDashBoardService.UpdateUser(user);
            if (x)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(editProfVM);
        }


    }
}

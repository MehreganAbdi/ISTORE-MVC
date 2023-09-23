﻿using I_STORE.Data;
using I_STORE.Models;
using I_STORE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
                                        ApplicationDbContext applicationDBContext)
        {
            _context = applicationDBContext;
            _signinManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Login()
        {

            //if we reload the page this will hold previos inserted values.

            var reloadSafety = new LoginVM();
            return View(reloadSafety);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passChecking = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passChecking)
                {
                    var res = await _signinManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (res.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "password must include Letters(a,A,b,B,..) , Numbers (0,1,2,...) And Other Signs(@,$,?,!,..).";
                return View(loginVM);
            }
            TempData["Error"] = "This User Doesn't Exist , Try To Register First";


            return View(loginVM);
        }



        public IActionResult Register()
        {
            //if we reload the page this will hold previos inserted values.

            var reloadSafety = new RegisterVM();
            return View(reloadSafety);

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);


            if (user != null)
            {
                TempData["Error"] = "This Email Already Exists";
                return View(registerVM);
            }
            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.UserName.Replace(" ",""),
                Address = new Address()
                {
                    FullAddress = registerVM.Address
                }

            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Home");


        }



        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

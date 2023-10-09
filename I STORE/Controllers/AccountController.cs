using Context.Data;
using Context.Models;
using Context.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity;

namespace I_STORE.Controllers
{
    public class AccountController : Controller
    {
        private  int? RegisterPassword ;

        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly Microsoft.AspNet.Identity.IIdentityMessageService _identityMessageService;
        public AccountController(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager,
                                        SignInManager<AppUser> signInManager,
                                        ApplicationDbContext applicationDBContext,
                                        IEmailService emailService,
                                        Microsoft.AspNet.Identity.IIdentityMessageService _identityMessageService)
        {
            _emailService = emailService;                               
            _context = applicationDBContext;
            _signinManager = signInManager;
            _userManager = userManager;
            this._identityMessageService = _identityMessageService;

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
                var emailInfo = new EmailDTO()
                {
                    subject = "Registration",
                    reciever = newUser.Email,
                    message = "Registration Completed! \n Ckeck Out Our New Sneakers On Our Web . \n"
                };
                await _emailService.SendEmail(emailInfo);
            }
            return RedirectToAction("Index", "Home");



        }
        

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GoogleOAuth(string code)
        {
            if (code != null)
            {
                var client = new RestClient("https://www.googleapis.com/oauth2/v4/token");
                var request = new RestRequest(Method.Post.ToString());
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("code", code);
                request.AddParameter("redirect_uri", "https://localhost:7141/Account/GoogleOAuth");

                request.AddParameter("client_id", "Enter your clientid here");
                request.AddParameter("client_secret", "Enter your client secret");

                var response = client.Execute(request);
                var content = response.Content;
                var res = (JObject)JsonConvert.DeserializeObject(content);
                var client2 = new RestClient("https://www.googleapis.com/oauth2/v1/userinfo");
                client2.AddDefaultHeader("Authorization", "Bearer " + res["access_token"]);

                request = new RestRequest(Method.Get.ToString());


                var response2 = client2.Execute(request);

                var content2 = response2.Content;

                var user = (JObject)JsonConvert.DeserializeObject(content2);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.ReturnData = "";
            }



            return View();
        }

        

        public async  Task<IActionResult> EmailConfirmation()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.Identity.GetUserId());
            var rnd = new Random();
            RegisterPassword = rnd.Next(999, 9999);
            var email = new EmailDTO()
            {
                message = "Your Confirmation Code : " + RegisterPassword.ToString(),
                subject = "Confirmation Code",
                reciever = user.Email
            };
            await _emailService.SendEmail(email);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EmailConfirmation(string Code)
        {
            if(RegisterPassword==null || RegisterPassword != Convert.ToInt32(Code) || Code=="" || Code==null)
            {
                RegisterPassword = null;
                return RedirectToAction("EditProfile", "UserDashBoard");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.Identity.GetUserId());
            user.EmailConfirmed = true;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        //[HttpGet]
        //public IActionResult AddPhoneNumber()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> AddPhoneNumber(string PhoneNumber)
        //{
        //    var rnd = new Random();
        //    RegisterPassword = rnd.Next(999, 9999);
        //    var user =await _context.Users.FirstOrDefaultAsync(u=>u.Id== User.Identity.GetUserId());
        //    user.PhoneNumber = PhoneNumber;
        //    var IdentityM = new Microsoft.AspNet.Identity.IdentityMessage()
        //    {
        //        Body = RegisterPassword.ToString(),
        //        Subject = "",
        //        Destination = PhoneNumber
        //    };
        //    _identityMessageService.SendAsync(IdentityM);
        //    return RedirectToAction("CodeConfirmation", "Account"); 
        //}
        //public IActionResult CodeConfirmation()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> CodeConfirmation(string Code )
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.Identity.GetUserId());
        //    if(Convert.ToInt32(Code) != RegisterPassword || RegisterPassword==null)
        //    {
        //        user.PhoneNumber = null;
        //        return RedirectToAction("AddPhoneNumber");
        //    }

        //    user.PhoneNumberConfirmed = true;

        //    RegisterPassword = null;
        //    return RedirectToAction("Index", "Home");
        //}
    }
}

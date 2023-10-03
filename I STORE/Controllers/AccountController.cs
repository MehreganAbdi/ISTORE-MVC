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
                SendEmail(newUser.Email, "Registration", "Registration Completed! \n Ckeck Out Our New Sneakers On Our Web . \n");
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
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("mehreganabdiwebmail@gmail.com");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "kxbo ipin pkyn vgfo";
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

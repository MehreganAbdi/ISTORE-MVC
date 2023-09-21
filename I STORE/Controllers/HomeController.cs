using I_STORE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace I_STORE.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
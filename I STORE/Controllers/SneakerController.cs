using I_STORE.Interfaces;
using I_STORE.Models;
using I_STORE.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class SneakerController : Controller
    {
        private readonly ISneakerService _sneakerService;
        public SneakerController(ISneakerService sneakerService)
        {
            _sneakerService = sneakerService;
        }

        public async Task<IActionResult> Index()
        {
            var allSneakers = await _sneakerService.GetAll();
            return View(allSneakers);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var sneaker = await _sneakerService.GetByIdAsync(Id);
            if(sneaker != null)
            {
                return View(sneaker);
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return  View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sneaker sneaker)
        {
            if (!ModelState.IsValid)
            {
                return View(sneaker);
            }
            _sneakerService.Add(sneaker);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int Id)
        {
            var sneaker = await _sneakerService.GetByIdAsync(Id);
            if (sneaker == null) return RedirectToAction("Index");
            var sneakerVM = new SneakerVM()
            {
                SneakerId = Id,
                Name = sneaker.Name,
                Company = sneaker.Company,
                Count = sneaker.Count,
                Size = sneaker.Size
            };

            return View(sneakerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SneakerVM sneakerVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var sneaker = new Sneaker()
            {
                SneakerId = sneakerVM.SneakerId,
                Name = sneakerVM.Name,
                Company = sneakerVM.Company,
                Size = sneakerVM.Size,
                Count = sneakerVM.Count
            };
            _sneakerService.Update(sneaker);
            
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Delete(int Id)
        {
            if(_sneakerService.Remove(await _sneakerService.GetByIdAsync(Id)))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}

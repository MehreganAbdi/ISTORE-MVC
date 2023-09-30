using Application.Interfaces;
using Context.Models;
using Context.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class SneakerController : Controller
    {
        private readonly ISneakerService _sneakerService;
        private readonly IPhotoService _photoService;
        public SneakerController(ISneakerService sneakerService, IPhotoService photoService)
        {
            _sneakerService = sneakerService;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index(string searching)
        {
            var allSneakers = await _sneakerService.GetAll();
            if (searching == null)
            {
                return View(allSneakers);
            }
            allSneakers = allSneakers.Where(s => s.Name.Contains(searching) ||
                                                s.Company.ToString().Contains(searching) ||
                                                s.Size.ToString().Contains(searching) ||
                                                s.Price.ToString().Contains(searching)).ToList();


            return View(allSneakers);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var sneaker = await _sneakerService.GetByIdAsync(Id);
            if (sneaker != null)
            {
                return View(sneaker);
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSneakerVM sneakerVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(sneakerVM.Image);
                var sneaker = new Sneaker()
                {
                    Company = sneakerVM.Company,
                    Name = sneakerVM.Name,
                    Size = sneakerVM.Size,
                    Count = sneakerVM.Count,
                    Image = result.Url.ToString(),
                    Price = sneakerVM.Price,
                    OFF = 0
                };

                _sneakerService.Add(sneaker);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }

            return View(sneakerVM);

        }


        public async Task<IActionResult> Edit(int Id)
        {
            var sneaker = await _sneakerService.GetByIdAsync(Id);
            if (sneaker == null) return RedirectToAction("Index");
            var sneakerVM = new CreateSneakerVM()
            {
                SneakerId = Id,
                Name = sneaker.Name,
                Company = sneaker.Company,
                Count = sneaker.Count,
                Price = sneaker.Price,
                Size = sneaker.Size,
                OFF = sneaker.OFF
            };

            return View(sneakerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateSneakerVM sneakerVM)
        {
            if (ModelState.IsValid)
            {
                var photoResult = await _photoService.AddPhotoAsync(sneakerVM.Image);

                var sneaker = new Sneaker()
                {
                    SneakerId = sneakerVM.SneakerId,
                    Name = sneakerVM.Name,
                    Price = sneakerVM.Price,
                    Company = sneakerVM.Company,
                    Size = sneakerVM.Size,
                    Count = sneakerVM.Count,
                    Image = photoResult.Url.ToString()
                    ,OFF=sneakerVM.OFF
                };
                _sneakerService.Update(sneaker);
                return RedirectToAction("Index");
            }
            
            return View(sneakerVM);


        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (_sneakerService.Remove(await _sneakerService.GetByIdAsync(Id)))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}

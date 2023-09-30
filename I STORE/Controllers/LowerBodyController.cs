using Application.Interfaces;
using Context.Models;
using Application.Services;
using Context.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class LowerBodyController : Controller
    {
        private readonly ILowerBodyService _lowerBodyService;
        private readonly IPhotoService _photoService;
        public LowerBodyController(IPhotoService photoService , ILowerBodyService lowerBodyService)
        {
            _lowerBodyService = lowerBodyService;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index(string searching)
        {
            var model = await _lowerBodyService.GetAll();
            if (searching == null)
            {
                return View(model);
            }
            model = model.Where(lb => lb.ProductName.Contains(searching)
                                  || lb.ProductCategory.ToString().Contains(searching)
                                  || lb.Size.Contains(searching)
                                  || lb.Price.ToString().Contains(searching)).ToList();
            return View(model);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            var product = await _lowerBodyService.GetByIdAsync(Id);
            return View(product);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            _lowerBodyService.Remove(await _lowerBodyService.GetByIdAsync(Id));
            return RedirectToAction("Index", "LowerBody");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(productVM.Image);

                var product = new Product()
                {
                    Price = productVM.Price,
                    ProductCategory = productVM.ProductCategory,
                    ProductName = productVM.ProductName,
                    Count = productVM.Count,
                    Image = result.Url.ToString(),
                    Size = productVM.Size,
                    OFF= 0
                };
            
                _lowerBodyService.Add(product);
                return RedirectToAction("Index", "LowerBody");
            }
            else
            {
                ModelState.AddModelError("", "Photo Failed To Upload");
            }
            return RedirectToAction("Index", "LowerBody");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var product = await _lowerBodyService.GetByIdAsync(Id);

            var productVM = new ProductVM()
            {
                ProductID = Id,
                ProductName = product.ProductName,

                Price = product.Price,
                ProductCategory = product.ProductCategory,
                Count = product.Count,
                Size = product.Size
                ,OFF = product.OFF
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(productVM.Image);

                var product = new Product()
                {
                    ProductID = productVM.ProductID,
                    Price = productVM.Price,
                    Count = productVM.Count,
                    Size = productVM.Size,
                    ProductName = productVM.ProductName,
                    ProductCategory = productVM.ProductCategory,
                    Image = result.Url.ToString(),
                    OFF = productVM.OFF
                };
                _lowerBodyService.Update(product);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Failed To Upload");
            }
            return View(productVM);
        }

    }
}

using Context.Data.Enum;
using Application.Interfaces;
using Context.Models;
using Application.Services;
using Context.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class UpperBodyController : Controller
    {
        private readonly IUpperBodyService _upperBodyService;
        private readonly IPhotoService _photoService;
        public UpperBodyController(IPhotoService photoService,IUpperBodyService upperBodyService)
        {
            _upperBodyService = upperBodyService;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index(string searching)
        {
            var model = await _upperBodyService.GetAll();
            model = model.Where(p => p.ProductCategory == ProductCategory.Hoody 
                                    || p.ProductCategory == ProductCategory.Tshirt 
                                    || p.ProductCategory == ProductCategory.Jacket).ToList();
            if (searching == null)
            {
                return View(model);
            }
            model = model.Where(ub => ub.ProductName.Contains(searching)
                                  || ub.ProductCategory.ToString().Contains(searching)
                                  || ub.Size.Contains(searching)
                                  || ub.Price.ToString().Contains(searching)).ToList();
            return View(model);
            
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var product = await _upperBodyService.GetByIdAsync(Id);
            return View(product);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            _upperBodyService.Remove(await _upperBodyService.GetByIdAsync(Id));
            return RedirectToAction("Index", "UpperBody");
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
                    Size = productVM.Size,
                    Image = result.Url.ToString(),
                    ProductCategory = productVM.ProductCategory,
                    ProductName = productVM.ProductName,
                    Count = productVM.Count
                    
                };

                _upperBodyService.Add(product);
                return RedirectToAction("Index", "UpperBody");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(productVM);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var product = await _upperBodyService.GetByIdAsync(Id);

            var productVM = new ProductVM()
            {
                ProductID = Id,
                ProductName = product.ProductName,
                
                Price = product.Price,
                ProductCategory = product.ProductCategory,
                Count = product.Count,
                Size = product.Size
                
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
                    Size = productVM.Size,
                    Image = result.Url.ToString(),
                    ProductCategory = productVM.ProductCategory,
                    ProductName = productVM.ProductName,
                    Count = productVM.Count

                };

                _upperBodyService.Update(product);
                return RedirectToAction("Index", "UpperBody");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(productVM);
        }

    }
}

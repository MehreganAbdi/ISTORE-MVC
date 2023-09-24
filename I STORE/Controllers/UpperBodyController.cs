using I_STORE.Data.Enum;
using I_STORE.Interfaces;
using I_STORE.Models;
using I_STORE.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class UpperBodyController : Controller
    {
        private readonly IUpperBodyService _upperBodyService;
        public UpperBodyController(IUpperBodyService upperBodyService)
        {
            _upperBodyService = upperBodyService;
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
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "UpperBody");
            }
            _upperBodyService.Add(product);
            return RedirectToAction("Index", "UpperBody");
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
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var product = new Product()
            {
                ProductID = productVM.ProductID,
                Price = productVM.Price,
                Count = productVM.Count,
                Size = productVM.Size,
                ProductName = productVM.ProductName,
                ProductCategory = productVM.ProductCategory
            };
            var result = _upperBodyService.Update(product);
            if (result)
            {
                return RedirectToAction("Index", "UpperBody");
            }
            return View(productVM);
        }

    }
}

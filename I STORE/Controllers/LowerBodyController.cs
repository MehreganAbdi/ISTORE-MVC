using I_STORE.Interfaces;
using I_STORE.Models;
using I_STORE.Services;
using I_STORE.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace I_STORE.Controllers
{
    public class LowerBodyController : Controller
    {
        private readonly ILowerBodyService _lowerBodyService;
        public LowerBodyController(ILowerBodyService lowerBodyService)
        {
            _lowerBodyService = lowerBodyService;
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
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "LowerBody");
            }
            _lowerBodyService.Add(product);
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
            var result = _lowerBodyService.Update(product);
            if (result)
            {
                return RedirectToAction("Index", "LowerBody");
            }
            return View(productVM);
        }

    }
}

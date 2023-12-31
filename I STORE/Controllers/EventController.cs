﻿using Application.Interfaces;
using Context.Models;
using Context.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace I_STORE.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<IActionResult> Index()
        {
            var events = await _eventRepository.GetAllAsync();
            return View(events);
        }
        public async Task<IActionResult> Create()
        {
            var sneakers = await _eventRepository.GetSneakers();
            var products = await _eventRepository.GetProducts();

            var eventtVM = new EventVM();
            eventtVM.allSneakers = sneakers.ToList();
            eventtVM.allProducts = products.ToList();
            return View(eventtVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(EventVM eventtVM)
        {
            var eventt = new Event()
            {
                Name = eventtVM.Name,
                ProductsEvents = eventtVM.SelectedProducts,
                SneakerEvents = eventtVM.SelectedSneakers,
                TimeRemaining = DateTime.Now.AddDays(14)
            };
            await _eventRepository.AddAsync(eventt);
            return RedirectToAction("Index", "Event");
        }

        public async Task<IActionResult> PSDetail(int Id)
        {
            var model = new PSDetailVM();
            var eventt = await _eventRepository.GetByIdAsync(Id);
            var products = eventt.ProductsEvents;
            var sneakers = eventt.SneakerEvents;
            if (products != null)
            {
                foreach (var item in products)
                {
                    model.Products.Add(item);
                }
            }
            if(sneakers != null)
            {
                foreach (var item in sneakers)
                {
                    model.Sneakers.Add(item);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int Id)
        {
            _eventRepository.Remove(await _eventRepository.GetByIdAsync(Id));
            return RedirectToAction("Index", "Event");
        }
    }
}

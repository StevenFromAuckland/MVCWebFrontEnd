using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using HillLabTestEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebUI.Models;

namespace MVCWebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductHttpClientWrapper _productHttpClientWrapper;
        private IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, IProductHttpClientWrapper productHttpClientWrapper, IMapper mapper)
        {
            _logger = logger;
            _productHttpClientWrapper = productHttpClientWrapper;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productHttpClientWrapper.Product.GetAllProducts();
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            await PrepareCategoryList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Quantity,Unit,CategoryId")] ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(productDTO);

                    await _productHttpClientWrapper.Product.CreateProduct(product);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return RedirectToAction(nameof(Index));
            }
            await PrepareCategoryList();
            return View(productDTO);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _productHttpClientWrapper.Product.GetProductById(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            var productDTO = _mapper.Map<ProductDTO>(result);

            await PrepareCategoryList();
            return View(productDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Quantity,Unit,CategoryId")] ProductDTO productDTO)
        {
            if (id != productDTO.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(productDTO);

                    await _productHttpClientWrapper.Product.UpdateProduct(product);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return RedirectToAction(nameof(Index));
            }

            await PrepareCategoryList();
            return View(productDTO);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            try
            {

                await _productHttpClientWrapper.Product.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return RedirectToAction(nameof(Index));

        }

        public async Task PrepareCategoryList()
        {
            var lstCategories = new List<Category>()
            {
                new Category() {CategoryId = 0, CategoryName = "--- Please select a category ---" }
            };
            var categories = await _productHttpClientWrapper.Category.GetAllCategories();
            if (categories != null && categories.Any())
                categories.ToList().ForEach(x => lstCategories.Add(x));
            ViewData["Categories"] = lstCategories;
        }

    }
}

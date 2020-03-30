using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using HillLabTestEntities;
using MVCWebUI.Models;

namespace MVCWebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IProductHttpClientWrapper _productHttpClientWrapper;
        private IMapper _mapper;

        public CategoriesController(ILogger<CategoriesController> logger, IProductHttpClientWrapper productHttpClientWrapper, IMapper mapper)
        {
            _logger = logger;
            _productHttpClientWrapper = productHttpClientWrapper;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productHttpClientWrapper.Category.GetAllCategories();
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = _mapper.Map<Category>(categoryDTO);

                    await _productHttpClientWrapper.Category.CreateCategory(category);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDTO);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _productHttpClientWrapper.Category.GetCategoryById(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            var categoryDTO = _mapper.Map<CategoryDTO>(result);

            return View(categoryDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = _mapper.Map<Category>(categoryDTO);

                    await _productHttpClientWrapper.Category.UpdateCategory(category);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDTO);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            try
            {

                await _productHttpClientWrapper.Category.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return RedirectToAction(nameof(Index));

        }
    }
}

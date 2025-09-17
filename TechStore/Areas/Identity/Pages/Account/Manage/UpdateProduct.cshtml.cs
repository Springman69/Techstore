using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechStore.Models;
using TechStore.Repositories;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class UpdateProductModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductSpecificationRepository _productSpecificationRepository;
        private readonly ILogger<UpdateProductModel> _logger;

        public UpdateProductModel(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductSpecificationRepository productSpecificationRepository,
            ILogger<UpdateProductModel> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productSpecificationRepository = productSpecificationRepository;
            _logger = logger;
        }

        [BindProperty]
        public int SelectedCategoryId { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public ProductSpecification ProductSpecification { get; set; }

        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int productId)
        {
            Product = _productRepository.GetProductById(productId);
            ProductSpecification = _productSpecificationRepository.GetSpecificationByProductId(productId);
            Categories = _categoryRepository.ReadCategories();


            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task OnPost()
        {

            var category =  _categoryRepository.GetCategoryById(Product.CategoryId);

            if (category != null)
            {

                var existingProduct = _productRepository.GetProductById(Product.Id);

                if (existingProduct != null)
                {
                    existingProduct.Name = Product.Name;
                    existingProduct.CategoryId = SelectedCategoryId;
                    existingProduct.Description = Product.Description;
                    existingProduct.Price = Product.Price;
                    existingProduct.Company = Product.Company;
                    existingProduct.Image = Product.Image;
                    existingProduct.Id = Product.Id;
                    if (_productRepository.UpdateProduct(existingProduct))
                    {
                        _logger.LogInformation($"Zaktualizowano adres: {existingProduct.Id}");
                    }
                    else
                    {
                        _logger.LogInformation($"Nie można zaktualizować adresu: {existingProduct.Id}");
                    }
                }
                else
                {
                    if (_productRepository.ChangeProduct(Product))
                    {
                        _logger.LogInformation($"Dodano adres: {Product.Id}");
                    }
                    else
                    {
                        _logger.LogInformation($"Nie można dodać adresu: {Product.Id}");
                    }
                }
            }
            else
            {
                _logger.LogInformation("Użytkownik nie został znaleziony.");
            }
        }
    }
}

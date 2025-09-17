using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TechStore.Models;
using TechStore.Repositories;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class AddProductModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductSpecificationRepository _productSpecificationRepository; 

        public AddProductModel(ICategoryRepository categoryRepository, IProductRepository productRepository, IProductSpecificationRepository productSpecificationRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productSpecificationRepository = productSpecificationRepository;  
        }


        [BindProperty]
        public int SelectedCategoryId { get; set; }

        public List<Category> Categories { get; set; }

        public IActionResult OnGet()
        {
            Categories = _categoryRepository.ReadCategories();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var product = new Product
            {
                Name = Request.Form["Name"],
                CategoryId = SelectedCategoryId,
                Quantity = int.Parse(Request.Form["Quantity"]),
                Price = decimal.Parse(Request.Form["Price"]),
                Image = Request.Form["Image"],
                Company = Request.Form["Company"]
            };

            await _productRepository.AddProductAsync(product);

            var addedProduct =  _productRepository.GetProductById(product.Id);

            if (addedProduct == null)
            {
                return Page();
            }

            var productSpecification = new ProductSpecification
            {
                Picture2 = Request.Form["Picture2"],
                Picture3 = Request.Form["Picture3"],
                Parametr1 = Request.Form["Parametr1"],
                Parametr2 = Request.Form["Parametr2"],
                Parametr3 = Request.Form["Parametr3"],
                Parametr4 = Request.Form["Parametr4"],
                Parametr5 = Request.Form["Parametr5"],
                Parametr6 = Request.Form["Parametr6"],
                Parametr7 = Request.Form["Parametr7"],
                Picture6 = Request.Form["Picture6"],
                Header1 = Request.Form["Header1"],
                Description1 = Request.Form["Description1"],
                Picture7 = Request.Form["Picture7"],
                Header2 = Request.Form["Header2"],
                Description2 = Request.Form["Description2"],
                Picture8 = Request.Form["Picture8"],
                Header3 = Request.Form["Header3"],
                Description3 = Request.Form["Description3"],
                ProductId = addedProduct.Id 
            };

            await _productSpecificationRepository.AddProductSpecificationAsync(productSpecification);

            return RedirectToPage(); 
        }
    }
}
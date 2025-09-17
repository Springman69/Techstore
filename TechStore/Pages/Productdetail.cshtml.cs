using TechStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechStore.Repositories;
using Microsoft.AspNetCore.Components;

namespace TechStore.Pages
{
    public class OpiniaPageModel : PageModel
    {
        private readonly IOpiniaRepository _opiniaRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductSpecificationRepository _specificationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OpiniaPageModel(IOpiniaRepository opiniaRepository, IProductRepository productRepository, IProductSpecificationRepository specificationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _opiniaRepository = opiniaRepository;
            _productRepository = productRepository;
            _specificationRepository = specificationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Opinia Opinia { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public ProductSpecification ProductSpecification { get; set; }
        public Product Product { get; private set; }
        public List<Product> ProductsWithSpecifications { get; set; }
        public List<ProductSpecification> Specifications { get; set; }

        public void OnGet()
        {
            Product = _productRepository.GetProductById(Id);
            ProductSpecification = _specificationRepository.GetByProductId(Id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Opinia.Data = DateTime.Now;

            await Task.Run(() => _opiniaRepository.AddAsync(Opinia));

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRateAsync(string category, int rating)
        {
            await Task.Run(() => _opiniaRepository.AddAsync(Opinia));

            return new JsonResult(new { success = true, message = "Ocena zapisana." });
        }

        public IActionResult OnPostAddToSession()
        {
            List<Product> sessionProductList;

            if (_httpContextAccessor.HttpContext.Session.Keys.Contains("ProductList"))
            {
                // Retrieve existing session list
                var productListJson = _httpContextAccessor.HttpContext.Session.GetString("ProductList");
                sessionProductList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(productListJson);
            }
            else
            {
                // Create a new session list if it doesn't exist
                sessionProductList = new List<Product>();
            }

            // Add the selected product to the session list
            Product selectedProduct = _productRepository.GetProductById(Id);
            sessionProductList.Add(selectedProduct);

            // Save the updated session list
            var updatedProductListJson = Newtonsoft.Json.JsonConvert.SerializeObject(sessionProductList);
            _httpContextAccessor.HttpContext.Session.SetString("ProductList", updatedProductListJson);

            return RedirectToPage();
        }
    }
}

using TechStore.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using System.Collections.Generic;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class ManageWarehouseModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        public List<Product> Products { get; private set; } = new List<Product>();

        public ManageWarehouseModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            Products = _productRepository.ReadProducts() ?? new List<Product>();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int productId)
        {
            await _productRepository.DeleteProductAsync(productId);

            Products = _productRepository.ReadProducts() ?? new List<Product>();

            return RedirectToPage();
        }
    }
}
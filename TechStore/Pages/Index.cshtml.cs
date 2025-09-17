using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStore.Models;
using TechStore.Repositories;

namespace TechStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductSpecificationRepository _specificationRepository;
        private readonly IProductRepository _productRepository;

        public List<Product> ProductsWithSpecifications { get; set; }

        public List<ProductSpecification> Specifications { get; set; }


        public IndexModel(IProductRepository productRepository, IProductSpecificationRepository specificationRepository)
        {
            _productRepository = productRepository;
            _specificationRepository = specificationRepository;
        }
        public void OnGet()
        {
            List<Product> productList = _productRepository.ReadProducts();

            Specifications = _specificationRepository.GetAll();

            ProductsWithSpecifications = productList.Select(product =>
            {
                product.Specification = Specifications.FirstOrDefault(spec => spec.ProductId == product.Id);
                return product;
            }).ToList();



        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStore.Repositories;
using TechStore.Models;
using TechStore.Data;
using Microsoft.EntityFrameworkCore;
namespace TechStore.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProductSpecificationRepository _specificationRepository;
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        [BindProperty]
        public List<string>? Options { get; set; }
        private int? _categoryid;
        private string indicator = "r";

		public List<Product> ProductsWithSpecifications { get; set; } = new List<Product>();
        public List<ProductSpecification> Specifications { get; set; } = new List<ProductSpecification>();
        public ProductsModel(IProductRepository productRepository, IProductSpecificationRepository productSpecificationRepository, ApplicationDbContext applicationDb)
        {
            _specificationRepository = productSpecificationRepository;
            _productRepository = productRepository;
            _applicationDbContext = applicationDb;
        }
        public void OnGet(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                _categoryid = categoryId;
                TempData["CategoryId"] = _categoryid;
            }
            else if (TempData.ContainsKey("CategoryId"))
            {
                _categoryid = (int)TempData["CategoryId"];
            }
            ProductsWithSpecifications = LoadData(_categoryid);
            Options = ProductsWithSpecifications.Select(p => p.Company).Distinct().ToList();

        }
		public void OnPostFiltrReview()
		{
			if (TempData.ContainsKey("CategoryId"))
			{
				_categoryid = (int)TempData["CategoryId"];
			}

			bool isDescending = TempData["IsDescending"] != null ? (bool)TempData["IsDescending"] : false;

			if (isDescending)
			{
				ProductsWithSpecifications = _applicationDbContext.Products
					.Include(x => x.Reviews)
					.Where(p => p.CategoryId == _categoryid)
					.OrderByDescending(p => p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0)
					.ToList();
			}
			else
			{
				ProductsWithSpecifications = _applicationDbContext.Products
					.Include(x => x.Reviews)
					.Where(p => p.CategoryId == _categoryid)
					.OrderBy(p => p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0)
					.ToList();
			}

			TempData["CategoryId"] = _categoryid;
			TempData["IsDescending"] = !isDescending; // Zmiana kierunku sortowania

			// Przekazanie informacji o kierunku sortowania do widoku
			ViewData["SortDirection"] = isDescending ? "Rosn¹co" : "Malej¹co";

			Options = ProductsWithSpecifications.Select(p => p.Company).Distinct().ToList();
		}

		public void OnPostFiltrName()
		{
			if (TempData.ContainsKey("CategoryId"))
			{
				_categoryid = (int)TempData["CategoryId"];
			}
			ProductsWithSpecifications = LoadData(_categoryid);
			ProductsWithSpecifications = ProductsWithSpecifications
				.Where(product => Options.Contains(product.Company))
				.ToList();
			if (Options.Count() == 0)
			{
				ProductsWithSpecifications = LoadData(_categoryid);
				Options = ProductsWithSpecifications.Select(p => p.Company).Distinct().ToList();
				TempData["CategoryId"] = _categoryid;
			}
			else
			{
				Options = LoadData(_categoryid).Select(p => p.Company).Distinct().ToList();
				TempData["CategoryId"] = _categoryid;
			}
		}


		private List<Product> LoadData(int? id)
        {
            var products = new List<Product>();
            products = _productRepository.ReadProducts();

            Specifications = _specificationRepository.GetAll();

            if (id.HasValue)
            {
                products = products
                    .Where(product => product.CategoryId == id)
                    .Select(product =>
                    {
                        product.Specification = Specifications.FirstOrDefault(spec => spec.ProductId == product.Id);
                        return product;
                    }).ToList();
                return products;
            }
            else
            {
                products = products.Select(product =>
                {
                    product.Specification = Specifications.FirstOrDefault(spec => spec.ProductId == product.Id);
                    return product;
                }).ToList();
                return products;
            }
        }

    }
}

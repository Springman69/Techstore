using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repositories;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class ManageProductsModel : PageModel
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ILogger<ManageProductsModel> logger;
        public ManageProductsModel(ICategoryRepository category, ILogger<ManageProductsModel> logger)
        {
            categoryRepository = category;
            this.logger = logger;

        }
        [BindProperty]
        public Category Category { get; set; } = new Category();
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if (categoryRepository.CreateCategory(Category))
            {
                logger.LogInformation($"Dodano Kategoriê: {Category.Name}");
            }
            else
            {
                logger.LogInformation($"Nie mo¿na dodaæ: {Category.Name}");
            }
        }
    }
}

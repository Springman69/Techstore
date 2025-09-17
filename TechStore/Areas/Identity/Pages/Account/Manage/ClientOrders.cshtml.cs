using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;


namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class ClientOrdersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;


        public ClientOrdersModel(ApplicationDbContext dbContext, UserManager<Client> userManager)
        {
            _userManager = userManager;
            _context = dbContext;
        }
        public List<Order> Orders { get; set; }
        public async Task OnGet()
        {
            Orders = await LoadData();
        }
        private async Task<List<Order>> LoadData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var orders = _context.Orders
                    .Include(x => x.ProductOrderRelations)
                    .ThenInclude(pr => pr.Product)
                    .Where(x => x.ClientId == user.Id.ToString())
                    .ToList();

                return orders;
            }
            // Zwróæ pust¹ listê lub obs³u¿ przypadek, gdy u¿ytkownik jest nullem
            return new List<Order>();
        }

    }
}

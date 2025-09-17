using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
using TechStore.Repositories;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class ManageOrdersModel : PageModel
    { 
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public string SelectedStatus { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }
        public List<string> OrderStatus { get; set; } = new List<string> { "W realizacji", "Przygotowane do wysy³ki", "Wys³ane", "Zrealizowane", "Wszystkie"};
        public ManageOrdersModel(ApplicationDbContext applicationDb)
        {
            
            _context = applicationDb;

        }
        public List<Order> Orders { get; set; }
        public void OnGet()
        {
            StartDate = DateTime.Today; // Ustawienie StartDate na dzisiejsz¹ datê
            EndDate = DateTime.Today;
            Orders = LoadData(); 
        }
        public void OnPostFiltrSelectedStatus()
        {
            StartDate = DateTime.Today; // Ustawienie StartDate na dzisiejsz¹ datê
            EndDate = DateTime.Today;
            Orders = LoadData();
            switch (SelectedStatus)
            {
                case "W realizacji":
                    Orders = Orders.Where(o => o.OrderStatus == "W realizacji").ToList();
                    break;
                case "Przygotowane do wysy³ki":
                    Orders = Orders.Where(o => o.OrderStatus == "Przygotowane do wysy³ki").ToList();
                    break;
                case "Zrealizowane":
                    Orders = Orders.Where(o => o.OrderStatus == "Zrealizowane").ToList();
                    break;
                case "Wys³ane":
                    Orders = Orders.Where(o => o.OrderStatus == "Wys³ane").ToList();
                    break;

                default:
                    
                    break;
            }
        }
        public void OnPostFiltrSelectedDateRange()
        {
            Orders = LoadData();
            Orders = Orders.Where(o => o.OrderDate.Date >= StartDate.Date && o.OrderDate.Date <= EndDate.Date).ToList();
            TempData["SelectedStartDate"] = StartDate;
            TempData["SelectedEndDate"] = EndDate;
        }
        private List<Order> LoadData() 
        {
            Orders = _context.Orders
               .Include(x => x.ProductOrderRelations)
               .ThenInclude(pr => pr.Product)
               .ToList();
            return Orders;
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
using TechStore.Repositories;


namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class OrderDetailsModel : PageModel
    {
        private int _orderId;
        private readonly ApplicationDbContext _context;
        private readonly IOrderRepository _orderRepository;
        public List<string> OrderStatus { get; set; } = new List<string> { "W realizacji", "Przygotowane do wysy³ki","Wys³ane","Zrealizowane" };
        private List<Order> Orders { get; set; }
        [BindProperty]
        public Order Order { get; set; }    
        [BindProperty]
        public string SelectedStatus { get; set; }
        public OrderDetailsModel(ApplicationDbContext context, IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }
        public void OnGet(int id)
        {
            TempData["OrderId"] = id.ToString();
            _orderId = id;
            Order = LoadData();
        }

        public void OnPostSaveSelectedStatus()
        {
            if (TempData.TryGetValue("OrderId", out var tempId) && int.TryParse(tempId.ToString(), out var orderId))
            {
                _orderId = orderId;
                Order = LoadData();
                Order.OrderStatus = SelectedStatus;
                _orderRepository.Update(Order);

                TempData["OrderId"] = _orderId.ToString(); // Zapisz OrderId w TempData
            }
        }

        private Order LoadData()
        {
            Orders = _context.Orders
               .Include(x => x.Client)
               .Include(x => x.ShippingAddress)
               .Include(x => x.ProductOrderRelations)
               .ThenInclude(pr => pr.Product)
               .ToList();
            Order = Orders.FirstOrDefault(o => o.Id == _orderId) ?? new Order();
            return Order;
        }
    }
}

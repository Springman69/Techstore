using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TechStore.Data;
using TechStore.Models;
using TechStore.Repositories;

namespace TechStore.Pages.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;
        private readonly IAddressRepository _addressRepository;
        public List<Product> SessionProductList { get; set; }
        public Order Order { get; set; } = new Order();
        public decimal OrderValue { get; set; }
        [BindProperty]
        public Client Client { get; set; } = new Client();
        [BindProperty]
        public Address Address { get; set; } = new Address();
        public IndexModel(ApplicationDbContext context, UserManager<Client> userManager, IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
            _context = context;
            _userManager = userManager;
        }
        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                Client = user;
                var existingAddress = _addressRepository.GetAddressByClientId(user.Id);

                if (existingAddress != null)
                {
                    Address = existingAddress;
                }
            }
            if (HttpContext.Session.Keys.Contains("ProductList"))
            {
                var productListJson = HttpContext.Session.GetString("ProductList");
                SessionProductList = JsonConvert.DeserializeObject<List<Product>>(productListJson);
                foreach(var p in SessionProductList)
                {
                    OrderValue += p.Price;
                }
            }

        }
        public async void OnPostTest()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                Client = user;
                var existingAddress = _addressRepository.GetAddressByClientId(user.Id);

                if (existingAddress != null)
                {
                    Address = existingAddress;
                }
            }
            if (HttpContext.Session.Keys.Contains("ProductList"))
            {
                var productListJson = HttpContext.Session.GetString("ProductList");
                SessionProductList = JsonConvert.DeserializeObject<List<Product>>(productListJson);
                foreach (var p in SessionProductList)
                {
                    OrderValue += p.Price;
                    
                }
            }
            ShippingAddress shippingAddress = new ShippingAddress();
            var maxId = _context.ShippingAddresses.Max(sa => (int?)sa.Id) ?? 0;
            shippingAddress.Id = maxId+1;
            shippingAddress.Street = Address.Street;
            shippingAddress.BuildingNumber = Address.BuildingNumber;
            shippingAddress.ApartmentNumber = Address.ApartmentNumber;
            shippingAddress.Locality = Address.Locality;
            shippingAddress.PostalCode = Address.PostalCode;
            _context.ShippingAddresses.Add(shippingAddress);
            _context.SaveChanges();
            Order.ShippingAddressId = shippingAddress.Id;
            Order.OrderStatus = "W realizacji";
            Order.OrderDate = DateTime.Now;
            Order.OrderValue = (double)OrderValue;
            Order.ClientId = Client.Id;
            Order.OrderConfirmation = true;
            Order.CompletionConfirmation = true;
            var OrderId = _context.Orders.Max(sa => (int?)sa.Id) ?? 0;
            Order.Id=OrderId+1;
            _context.Orders.Add(Order);
            _context.SaveChanges();
            ProductOrderRelation productOrderRelation = new ProductOrderRelation();
            foreach (var p in SessionProductList)
            {
                productOrderRelation.ProductId = p.Id;
                productOrderRelation.OrderId = Order.Id;
                productOrderRelation.Quantity = 1;
                _context.ProductOrderRelations.Add(productOrderRelation);
                _context.SaveChanges();

            }
        }
    }
}

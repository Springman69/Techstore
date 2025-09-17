using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TechStore.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TechStore.Pages.Cart
{
    
    public class CartModel : PageModel
    {

        public List<Product> SessionProductList { get; set; }
        public void OnGet()
        {
            if (HttpContext.Session.Keys.Contains("ProductList"))
            {
                var productListJson = HttpContext.Session.GetString("ProductList");
                SessionProductList = JsonConvert.DeserializeObject<List<Product>>(productListJson);
            }
        }
    }
    }
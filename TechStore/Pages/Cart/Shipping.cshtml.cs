using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TechStore.Models;

namespace TechStore.Pages.Cart
{
    [Authorize]
    public class ShippingModel : PageModel
    {
        public void OnGet()
        {
            // Pobierz identyfikator u¿ytkownika
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Pobierz nazwê u¿ytkownika
            string userName = User.Identity.Name;

            // Inne informacje o u¿ytkowniku mo¿na równie¿ uzyskaæ przez obiekt User

            // Tutaj mo¿esz u¿yæ pobranych informacji o u¿ytkowniku
        }
    }
}

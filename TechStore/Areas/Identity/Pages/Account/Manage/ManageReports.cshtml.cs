using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class ManageReportsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ManageReportsModel(ApplicationDbContext applicationDb)
        {
            _context = applicationDb;
        }
        public List<Report> Reports { get; set; }
        public void OnGet()
        {
            Reports = _context.Reports
                .Include(x=>x.Client)
                .ToList() ?? new List<Report>();
        }
    }
}

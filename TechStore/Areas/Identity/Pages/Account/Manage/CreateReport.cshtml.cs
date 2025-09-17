using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStore.Models;
using TechStore.Repositories;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class CreateReportModel : PageModel
    {
        private readonly IReportRepository _reportRepository;
        private readonly UserManager<Client> _userManager;

        public CreateReportModel(IReportRepository reportRepository, UserManager<Client> userManager)
        {
            _reportRepository = reportRepository;
            _userManager = userManager;
        }
        [BindProperty]
        public Report report { get; set; } = new Report();
        public void OnGet()
        {
        }
        public async void OnPost() 
        {
            Client client = await _userManager.GetUserAsync(User);
            _reportRepository.CreateReport(report, client);
        }
    }
}

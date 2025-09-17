using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Identity;
using TechStore.Models;
using TechStore.Repositories;
using Microsoft.AspNetCore.Authorization;
namespace TechStore.Pages.Kontakt;

public class KontaktModel : PageModel
{
    private readonly IReportRepository _reportRepository;
    private readonly UserManager<Client> _userManager;

    public KontaktModel(IReportRepository reportRepository, UserManager<Client> userManager)
    {
        _reportRepository = reportRepository;
        _userManager = userManager;
    }
    [BindProperty]
    public Report report { get; set; } = new Report();
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var client = await _userManager.GetUserAsync(User);

        if (client == null)
        {
            return Redirect("/Identity/Account/Login");
        }

        _reportRepository.CreateReport(report, client);

        return RedirectToPage("/Kontakt"); // Mo¿esz przekierowaæ gdzieœ indziej po utworzeniu raportu
    }
}


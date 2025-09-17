using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repositories;
using TechStore.Services;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class ReportDetailsModel : PageModel
    {
        private readonly IReportRepository reportRepository;
        private readonly EmailService _emailSender;

        [BindProperty]
        public InputModel Input { get; set; }
        public ReportDetailsModel(IReportRepository repository, EmailService emailService)
        {
            reportRepository = repository;
            _emailSender = emailService;
        }
        public Report Report { get; set; }
        public void OnGet(int id)
        {
            Report = reportRepository.GetReport(id);
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                Report = reportRepository.GetReport(id);
                return Page();
            }
            Report = reportRepository.GetReport(id);
            Report.Answered = true;
            reportRepository.UpdateReport(Report);
            _emailSender.SendEmailAsync(Report.Client.Email, $"Techstore - Odpowiedü na temat: {Report.Title}", Input.Answer);
            return RedirectToPage("./ManageReports");
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Pole Odpowiedü jest wymagane")]
            public string Answer {  get; set; }
        }
    }
}

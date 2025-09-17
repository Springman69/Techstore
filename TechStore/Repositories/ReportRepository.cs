using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;
        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateReport(Report report, Client client)
        {
            if (report != null && client != null)
            {
                report.ClientId = client.Id;
                report.Answered = false;
                _context.Reports.Add(report);

                return _context.SaveChanges() > 0 ? true:false ; // Zwraca true jeśli coś zostało zmienione w bazie danych
            }

            return false; // Zwraca false, jeśli report lub client jest null
        }

        public Report GetReport(int id)
        {
            Report report;
            report = _context.Reports.Include(x => x.Client).FirstOrDefault(r => r.Id == id);
            return report;
        }

        public bool UpdateReport(Report report)
        {
            _context.Reports.Update(report);
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}

using TechStore.Models;

namespace TechStore.Repositories
{
    public interface IReportRepository
    {
        public Report GetReport(int id);
        public bool CreateReport(Report report, Client client);
        public bool UpdateReport(Report report);
    }
}

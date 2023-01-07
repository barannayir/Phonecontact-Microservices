using ReportService.Data.Interfaces;

namespace ReportService.Data
{
    public class BaseEntity : IEntity
    {
        public string Uuid { get ; set ; }
    }
}

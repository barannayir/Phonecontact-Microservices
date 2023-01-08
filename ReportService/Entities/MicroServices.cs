namespace ReportService.Entities
{
    public class MicroServices
    {
        public ServiceInfo ContactService { get; set; }
        public ServiceInfo ReportService { get; set; }
    }

    public class ServiceInfo
    {
        public string Domain { get; set; }
    }
}
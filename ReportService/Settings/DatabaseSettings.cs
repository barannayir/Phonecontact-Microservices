namespace ReportService.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
        public string ReportCollectionName { get; set; }
    }
}
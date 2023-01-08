namespace ReportService.Settings
{
    public interface IDatabaseSettings
    {
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
        public string ReportCollectionName { get; set; }
    }
}
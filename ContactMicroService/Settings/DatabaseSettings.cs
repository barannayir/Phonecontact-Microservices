namespace ContactMicroService.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
        public string ContactCollectionName { get; set; }
        public string CommunicationCollectionName { get; set; }
    }
}
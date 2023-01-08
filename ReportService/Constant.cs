namespace ReportService
{
    public class Constant
    {
        #region Report API

        public const string ReportUpdateUrl = "api/Reports";

        #endregion Report API

        #region Contact API

        public const string ContactGetReportData = "api/Contact/GetContactStatistics";

        #endregion Contact API

        #region RabitMQ

        public const string ReportExchange = "report.direct.exchange";
        public const string ReportRouting = "report.route";
        public const string ReportQueue = "report.queue";

        #endregion RabitMQ
    }
}
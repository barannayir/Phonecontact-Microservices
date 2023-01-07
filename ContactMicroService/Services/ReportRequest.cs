using ContactMicroService.Entities;
using ContactMicroService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace ContactMicroService.Services
{
    public class ReportRequest : IReportRequest
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ReportRequest(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Response> SendReportRequest(Contact contact)
        {
            var httpContent = new StringContent(
            JsonConvert.SerializeObject(contact.Location),
            Encoding.UTF8,
            "application/json"
             );
            var response = await _httpClient.PostAsync($"{_configuration["ReportService"]}/api/Report/ReportRequest", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var genericData = JsonConvert.DeserializeObject<Response>(data);
                return genericData;
            }
            else
            {
                return null;
            }

        }
    }
}

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReportService.Entities.Dtos;
using ReportService.Http.Interface;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReportService.Http
{
    public class ContactClient : IContactClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ContactClient(HttpClient client, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = client;
        }
        public async Task<IEnumerable<ContactInfo>> GetAllInformations()
        {
            List<ContactInfo> contactInformationList = new List<ContactInfo>();
            var response = await _httpClient.GetAsync($"{_configuration["ContactService"]}/api/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                contactInformationList = JsonConvert.DeserializeObject<List<ContactInfo>>(responseData);
            }
            return contactInformationList;
        }
    }
}

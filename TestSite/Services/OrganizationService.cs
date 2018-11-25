using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TestSite.Model;

namespace TestSite.Services
{
    public class OrganizationService : IOrganizationService
    {
        #region Private member

        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceUrl;

        #endregion

        #region Constructor

        public OrganizationService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            _remoteServiceUrl = $"{_settings.Value.OrganizationUrl}/api/organization/";
        }

        #endregion

        #region Methods and Tasks

        public async Task<HttpResponseMessage> Create(CatalogItem details)
        {
            var content = new StringContent(JsonConvert.SerializeObject(details), Encoding.UTF8, "application/json");
            var responseString = await _httpClient.PostAsync(_remoteServiceUrl, content);
            return responseString;
        }

        public async Task<HttpStatusCode> Delete(int id)
        {
            var url = _remoteServiceUrl + id;
            var responseString = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, url));
            return responseString.StatusCode;
        }

        public async Task<CatalogItem> Get(int id)
        {
            var url = _remoteServiceUrl + id;
            var responseString = await _httpClient.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<CatalogItem>(responseString);
            
            return result;
        }

        #endregion
    }
}
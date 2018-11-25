using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestSite.Model;

namespace TestSite.Services
{
    /// <summary>
    /// Класс реализующий интерфейс ICatalogService. 
    /// Нужен для получения данных от сервиса Catalog.Service
    /// </summary>
    public class CatalogService : ICatalogService
    {
        #region Private member

        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceUrl;

        #endregion

        #region Constructor

        public CatalogService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            _remoteServiceUrl = $"{_settings.Value.CatalogUrl}/api/catalog";
        }

        #endregion

        #region Methods and Tasks

        public async Task<Catalog> GetAll(string searchString) // задача возвращающая объект класса каталог
        {
            var searchUrl = !string.IsNullOrWhiteSpace(searchString) ? _remoteServiceUrl + "/search?searchString=" + searchString : _remoteServiceUrl;

            var responseString = await _httpClient.GetStringAsync(searchUrl);

            var catalogItems = JsonConvert.DeserializeObject<List<CatalogItem>>(responseString);
            var catalog = new Catalog
            {
                CatalogItems = catalogItems
            };
            return catalog;
        }

        #endregion
    }
}

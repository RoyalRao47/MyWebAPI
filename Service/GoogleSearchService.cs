using Microsoft.EntityFrameworkCore;
using MyWebAPI.DataBase;
using MyWebAPI.Models;
using Newtonsoft.Json;

namespace MyWebAPI.Service
{
    public class GoogleSearchService :IGoogleSearchService
    {
        private bool disposedValue = false;
        private readonly string _apiKey;
        private readonly string _searchEngineId;
        private readonly string _baseURL;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public GoogleSearchService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = configuration["GoogleSearch:ApiKey"];
            _searchEngineId = configuration["GoogleSearch:SearchEngineId"];
            _baseURL = configuration["GoogleSearch:BaseUrl"];
            _httpClient = httpClient;
        }

        public async Task<GoogleSearchModel> GoogleSearchAsync(string query, int PageSize, int PageNumber)
        {
            var url = _baseURL + $"{Uri.EscapeDataString(query)}&key={_apiKey}&cx={_searchEngineId}&num={PageSize}&start={PageNumber}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleSearchModel>(content);
        }

        public async Task<GoogleSearchModel> GoogleSearchHindiAsync(string query, string LanguageCode)
        {
            LanguageCode = "Lang_" + LanguageCode;
            var url = _baseURL + $"{Uri.EscapeDataString(query)}&key={_apiKey}&cx={_searchEngineId}&lr={LanguageCode}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleSearchModel>(content);
        }

        public async Task<GoogleSearchModel> GoogleSearchTypeAsync(string query, string type)
        {
            var url = _baseURL + $"{Uri.EscapeDataString(query)}&key={_apiKey}&cx={_searchEngineId}";
            if (type.ToLower().Contains("image"))
            {
                url = url + "&searchType=image";
            }
            else if (type.ToLower().Contains("file"))
            {
                url = url + "&fileType=pdf";
            }
            else if (type.ToLower().Contains("safe"))
            {
                url = url + "&safe=active";
            }

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleSearchModel>(content);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}

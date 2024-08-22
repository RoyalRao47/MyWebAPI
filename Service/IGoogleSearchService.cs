using MyWebAPI.Models;

namespace MyWebAPI.Service
{
    public interface IGoogleSearchService :IDisposable
    {
        Task<GoogleSearchModel> GoogleSearchAsync(string query, int PageSize, int PageNumber);
        Task<GoogleSearchModel> GoogleSearchHindiAsync(string query, string language);
        Task<GoogleSearchModel> GoogleSearchTypeAsync(string query, string type);
    }
}

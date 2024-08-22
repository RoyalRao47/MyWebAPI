using MyWebAPI.Models;

namespace MyWebAPI.Service
{
	public interface IRestCountriesService : IDisposable
	{
		Task<RestCountries> GetCountriesAll();
		Task<ExchangeRateResponse> GetExchnageRate(string currencyCode);
		Task<ExchangeConvertorResponse> GetExchnageRateConevertor(string FromCurrencyCode, string ToCurrencyCode);

		Task<ExchangeConvertorResponse> GetGeoLocation(string ipAddress);
		Task<ExchangeConvertorResponse> GetGeoAstro(string Location);

		Task SendSmsAsync(string to, string message);
		Task MakeCallAsync(string to, string url);

		Task<JokesModel> GetJokes(PagingModel model);

    }
}

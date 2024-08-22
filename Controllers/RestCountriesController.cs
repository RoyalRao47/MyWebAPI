using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using MyWebAPI.Service;

namespace MyWebAPI.Controllers
{
	[Route("api/")]
	[ApiController]
	public class RestCountriesController : ControllerBase
	{
		private readonly IRestCountriesService _restCountriesService;

		public RestCountriesController(IRestCountriesService restCountriesService)
		{
			_restCountriesService = restCountriesService;
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet("GetAllCountries")]
		public async Task<IActionResult> GetAllCountries()
		{
			var searchResult = await _restCountriesService.GetCountriesAll();
			return Ok(searchResult);
		}

		[HttpGet("GetExchangeRate")]
		public async Task<IActionResult> GetExchnageRate(string currencyCode)
		{
			var searchResult = await _restCountriesService.GetExchnageRate(currencyCode);
			return Ok(searchResult);
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet("GetCurrencyConvertor")]
		public async Task<IActionResult> GetExchnageRate(string BaseCurrency , string TargerCurrency)
		{
			var searchResult = await _restCountriesService.GetExchnageRateConevertor(BaseCurrency, TargerCurrency);
			return Ok(searchResult);
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet("GetIpAddressLocation")]
		public async Task<IActionResult> GetIpAddressLocation(string ipAddress)
		{
			var searchResult = await _restCountriesService.GetGeoLocation(ipAddress);
			return Ok(searchResult);
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet("GetGeoAstronomy")]
		public async Task<IActionResult> GetGeoAstronomy(string Location)
		{
			var searchResult = await _restCountriesService.GetGeoAstro(Location);
			return Ok(searchResult);
		}

        [HttpGet("GetJokes")]
        public async Task<IActionResult> GetJokes([FromQuery] PagingModel model)
        {
            var searchResult = await _restCountriesService.GetJokes(model);
            return Ok(searchResult);

            //var searchResult = await _restCountriesService.GetJokes();
            //return Ok(searchResult);
        }
    }
}

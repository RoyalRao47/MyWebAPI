using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Service;
using static System.Net.WebRequestMethods;

namespace MyWebAPI.Controllers
{
	[Route("api/")]
	[ApiController]
	public class TwilioController : ControllerBase
	{
		private readonly IRestCountriesService _restCountriesService;

		public TwilioController(IRestCountriesService restCountriesService)
		{
			_restCountriesService = restCountriesService;
		}

		[HttpPost("Send-SMS")]
		public async Task<IActionResult> SendSms([FromQuery] string to, [FromBody] string message)
		{
			try
			{
				await _restCountriesService.SendSmsAsync(to, message);
				return Ok("SMS sent successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("Make-Call")]
		public async Task<IActionResult> MakeCall([FromQuery] string to)
		{
			try
			{
				string url = "http://demo.twilio.com/docs/voice.xml";
				await _restCountriesService.MakeCallAsync(to, url);
				return Ok("Call initiated successfully.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.DataBase;
using MyWebAPI.Models;
using MyWebAPI.Service;

namespace MyWebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class GoogleSearchController : ControllerBase
    {
        private readonly IGoogleSearchService _googleSearchService;

        public GoogleSearchController(IGoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }
        [HttpGet("SearchGoogle")]
		[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, NoStore = false)]
        [ProducesResponseType(typeof(List<GoogleSearchModel>), StatusCodes.Status200OK)] // 200 OK
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)] // 400 Bad Request
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)] // 404 Not Found
        public async Task<IActionResult> SearchGoogle(string query, int index, int count)
        {
            var searchResult = await _googleSearchService.GoogleSearchAsync(query, count, index);
            return Ok(searchResult);
        }
        [HttpGet("SearchGoogleLang")]
        public async Task<IActionResult> SearchGoogleLang(string SearchItem, string LanguageCode)
        {
            var searchResult = await _googleSearchService.GoogleSearchHindiAsync(SearchItem, LanguageCode);
            return Ok(searchResult);
        }
        [HttpGet("SearchGoogleType")]
        public async Task<IActionResult> SearchGoogleType(string SearchItem, string Type)
        {
            var searchResult = await _googleSearchService.GoogleSearchTypeAsync(SearchItem, Type);
            return Ok(searchResult);
        }
    }
}

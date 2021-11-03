using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Search.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly ILogger<SearchController> logger;

        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            this.searchService = searchService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm searchTerm)
        {
            var result = await searchService.SearchAsync(searchTerm.CustomerId);
            logger.LogWarning("In search controller");
            if (result.IsSucc)
                return Ok(result.SearchResult);
            return NotFound();
        }
    }
}
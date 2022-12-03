using ECommerce.API.Search.Interface;
using ECommerce.API.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchControler : ControllerBase
    {
        public readonly ISearchService searchService;

        public SearchControler(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await searchService.SearchAsync(term.CustomerId);
            if (result.isSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.Service.Model;
using Catalog.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Catalog.Service.Controllers
{
    [Route("api/[controller]")]
    public class CatalogController : Controller
    {
        private readonly OrganizationProvider _organizationProvider;

        public CatalogController(OrganizationProvider organizationProvider)
        {
            _organizationProvider = organizationProvider;
        }
        // GET: api/Catalog
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var responseString = await _organizationProvider.GetAll(null);

            return Ok(responseString);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("Search")]
        public async Task<IActionResult> Search(string searchString)
        {
            var responseString = await _organizationProvider.GetAll(searchString);

            return Ok(responseString);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using TestSite.Model;
using TestSite.Services;

namespace TestSite.Controllers
{
    public class OrganizationController : Controller
    {
        private IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService) => 
            _organizationService = organizationService ?? throw new ArgumentNullException(nameof(organizationService));

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("А id кто будет указывать?");
            }
            var view = await _organizationService.Get((int)id);
            if (view == null)
            {
                return NotFound("Ничего не найдено!!!");
            }
            return View(view);
        }

        // GET: Organization/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organization/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatalogItem collection)
        {
            var request = await _organizationService.Create(collection);

            if (request.StatusCode != HttpStatusCode.BadRequest)
                return RedirectToAction(nameof(CatalogController.Index), "Catalog");

            var jsonResult = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(request.Content.ReadAsStringAsync().Result);

            foreach (var key in jsonResult)
            {
                foreach (var error in key.Value)
                {
                    ModelState.AddModelError(key.Key, error);
                }
            }

            return View(collection);

        }
    }
}
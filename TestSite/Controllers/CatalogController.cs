using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using TestSite.Services;

namespace TestSite.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IOrganizationService _organizationService;

        public CatalogController(ICatalogService catalogService, IOrganizationService organizationService)
        {
            _catalogService = catalogService;
            _organizationService = organizationService;
        }

        // GET: Catalog
        public async Task<ActionResult> Index()
        {
            var view = await _catalogService.GetAll(null);
            return View(view.CatalogItems);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return RedirectToAction("Index");
            }
            var view = await _catalogService.GetAll(searchString);
            return View("Index", view.CatalogItems);
        }

        // POST: Catalog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("А id кто будет указывать?");
            }

            var result = await _organizationService.Delete((int) id);

            if (result != HttpStatusCode.OK) return NotFound("Что то явно пошло не так");

            return RedirectToAction("Index");
        }
    }
}
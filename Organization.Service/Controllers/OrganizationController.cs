using Microsoft.AspNetCore.Mvc;
using Organization.Service.Model;
using Organization.Service.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Organization.Service.Controllers
{
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private readonly IRepository _repository;

        public OrganizationController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Organization
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Organization/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repository.Get(id);
            return Ok(result);
        }
        
        // POST: api/Organization
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrganizationDetails value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var query = await _repository.Create(value);
            if (query != "Ok")
            {
                return BadRequest(query);
            }
            return Ok();
        }
        
        // PUT: api/Organization/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("А всё таки, id указывать то кто будет?");
            }

            var resultDelete = await _repository.Delete((int) id);

            return !resultDelete ? StatusCode(404) : Ok();
        }
    }
}

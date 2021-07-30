using Catalog.Api.Entities;
using Catalog.Api.Reposes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepos _repos;

        public ProductController(IProductRepos repos)
        {
            _repos = repos;
        }

        // GET: api/<CatalogController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return Ok(await _repos.Get());
        }

        // GET: api/GetByName/{name}
        [HttpGet]
        [Route("/GetByName/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetByName(string name)
        {
            return Ok(await _repos.GetByName(name));
        }

        // GET: api/GetByName/{category}
        [HttpGet]
        [Route("/GetByCategory/{category}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string category)
        {
            return Ok(await _repos.GetByCategory(category));
        }

        // GET api/<CatalogController>/id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var item = await _repos.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/<CatalogController>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Post([FromBody] Product item)
        {
            var id = await _repos.Insert(item);

            return CreatedAtAction("Get", new { Id = id }, item);
        }

        // PUT api/<CatalogController>/id
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> Put(string id, [FromBody] Product item)
        {
            return Ok(await _repos.Update(id, item));
        }

        // DELETE api/<CatalogController>/id
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            return Ok(await _repos.Delete(id));
        }

        [HttpGet]
        [Route("/ping")]
        public ActionResult<string> Ping() {
            return Ok("pong");
        }
    }
}

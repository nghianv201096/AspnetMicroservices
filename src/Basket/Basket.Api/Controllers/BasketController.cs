using Basket.Api.Entities;
using Basket.Api.Reposes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepos _repos;

        public BasketController(IBasketRepos repos)
        {
            _repos = repos;
        }

        // GET: api/<BasketController>/nghiaNv
        [HttpGet]
        [Route("{username}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> Get(string username)
        {
            var item = await _repos.Get(username);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/<BasketController>
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> Post([FromBody] ShoppingCart item)
        {
            await _repos.Save(item);

            return CreatedAtAction("Get", new { userName = item.UserName }, item);
        }

        // PUT api/<BasketController>/nghiaNv
        [HttpPut("{username}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> Put(string username, [FromBody] ShoppingCart item)
        {
            item.UserName = username;
            await _repos.Save(item);

            return Ok(true);
        }

        // DELETE api/<BasketController>/nghiaNv
        [HttpDelete("{username}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> Delete(string username)
        {
            await _repos.Delete(username);

            return Ok(true);
        }
    }
}

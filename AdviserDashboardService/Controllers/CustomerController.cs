using Microsoft.AspNetCore.Mvc;
using CustomerDashboardService.Model;
using CustomerDashboardService.Data.Repositories;
using System.Threading.Tasks;
using System;

namespace CustomerDashboardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository repository;

        public CustomerController(CustomerRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/[controller]/A57592E3-D03B-4F73-A6F3-FB9BC3CC5CD8       
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var entity = await repository.Get(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        // PUT: api/[controller]/A57592E3-D03B-4F73-A6F3-FB9BC3CC5CD8
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Customer entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            await repository.Update(entity);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer entity)
        {
            await repository.Add(entity);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        // DELETE: api/[controller]/A57592E3-D03B-4F73-A6F3-FB9BC3CC5CD8       
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(string id)
        {
            var entity = await repository.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp.Test;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAppController : ControllerBase
    {
        public IMediator Mediator { get; }

        public TaskAppController(IMediator mediator)
        {
            Mediator = mediator;
        }

        // GET: api/<TaskAppController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TaskAppController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TaskAppController>
        [HttpPost]
        public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskCommand command)
        {
            // Send may return a response, but do not have to do it.
            // Publish never return the result.
            await Mediator.Send(command);
            return CreatedAtAction(nameof(CreateTaskItem),new { Messaage = "Successfully created" });
        }

        // PUT api/<TaskAppController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TaskAppController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

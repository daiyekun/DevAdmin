using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WooDev.WebApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class DevUserController : ControllerBase
    {
        // GET: api/<DevUserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DevUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DevUserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DevUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DevUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

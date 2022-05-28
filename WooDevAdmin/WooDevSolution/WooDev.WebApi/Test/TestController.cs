using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;



namespace WooDev.WebApi.Test
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class TestController : ControllerBase
    {
        private  IDevUserService _IDevUserService;
        public TestController(IDevUserService iDevUserService)
        {
            _IDevUserService = iDevUserService;
        }

        [HttpGet("GetUsers")]
        public string  GetUsers()
        {
            var query = _IDevUserService.Query().ToList();
            var jsonstr = JsonUtility.SerializeObject(query);
            return jsonstr;
        }

        // GET: api/<TestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

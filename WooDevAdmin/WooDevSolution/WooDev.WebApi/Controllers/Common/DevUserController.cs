using Dev.WooNet.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WooDev.Auth.Model;
using WooDev.IServices;
using WooDev.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WooDev.WebApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class DevUserController : ControllerBase
    {
        private IDevUserService _IDevUserService;
        public DevUserController(IDevUserService iDevUserService)
        {
            _IDevUserService = iDevUserService;
        }
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

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        [Route("query")]
        [HttpGet]
        [AllowAnonymous]//跳过授权验证

        public IActionResult QueryUser(string username, string password)
        {

            AjaxResult<LoginResult> ajaxResult = null;
            var result = _IDevUserService.Login(username, password);

            ajaxResult = new AjaxResult<LoginResult>()
            {
                Result = true,
                data = result
            };
            return new JsonResult(ajaxResult);


        }
    }
}

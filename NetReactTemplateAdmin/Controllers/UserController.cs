using Microsoft.AspNetCore.Mvc;

namespace NetReactTemplateAdmin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("AddUser")]
        public ActionResult AddUser()
        {
            return Ok();
        }

        [HttpDelete("RemoveUser")]
        public ActionResult RemoveUser([FromBody] int userId)
        {
            return Ok();
        }

        [HttpPost("UpdateUser")]
        public ActionResult UpdateUser()
        {
            return Ok();
        }

        [HttpGet("GetUsers")]
        public ActionResult GetUsers()
        {
            return Ok();
        }

        [HttpGet("GetUsersByName")]
        public ActionResult GetUsersByName()
        {
            return Ok();
        }
    }
}

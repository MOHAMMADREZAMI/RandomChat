using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheRandomChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterName : ControllerBase
    {

        [HttpPost("RegisterName")]
        public ActionResult<string> RegisterName(string username)
        {
            //variables

            //variables




        }
    }
}

using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheRandomChat.Services;

namespace TheRandomChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterName : ControllerBase
    {

        [HttpPost("RegisterName")]
        public IActionResult registerName(string username)
        {
            //variables
            AuthorizationJwt authorization = new AuthorizationJwt();
            AccountControlService accountControl = new AccountControlService();
            //variables

            if (accountControl.CheckUserExists(username))
                return Conflict("this username is already taken");
            else
                return Ok(new { Url = "sd", Token = authorization.CreateJwt(username)});
        }
    }
}

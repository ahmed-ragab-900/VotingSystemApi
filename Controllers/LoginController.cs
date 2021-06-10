using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Body;
using VotingSystemApi.Services;

namespace VotingSystemApi.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginServices _loginServices = new LoginServices();

        [Route("api/[controller]/SignIn"), HttpPost]
        public object SignIn([FromBody] PostSignInDTO dto)
        {
            if (ModelState.IsValid)
            {
                string path3 = Startup.contentRoot;

                ResponseDTO res = _loginServices.SignIn(dto);
                return res;
            }
            else
            {
                return BadRequest("Incorrect model data");
            }
        }

        //[Route("api/[controller]/SignUp"), HttpPost]
        //public object SignUp([FromBody] PostSignUpDTO dto) { }

        //[Route("api/[controller]/SignUp"), HttpPost]
        //public object RefreshToken() { }
    }
}

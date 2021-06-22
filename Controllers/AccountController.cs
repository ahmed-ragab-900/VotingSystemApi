using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.User;
using VotingSystemApi.Services.Account;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountServices accountServices;

        public AccountController(IAccountServices accountServices, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.accountServices = accountServices;
        }

        [Route("SignIn"), HttpPost]
        public object SignIn([FromBody] SignInDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                ResponseDTO res = accountServices.SignIn(ServerRootPath ,dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong + $"\n {ex.Message}");
            }
        }
        
        [Route("SignUp"), HttpPost]
        public object SignUp([FromBody] AddUserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                ResponseDTO res = accountServices.SignUp(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Authorize]
        [Route("RefreshToken"), HttpGet]
        public object RefreshToken()
        {
            try
            {
                string userId = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value;
                ResponseDTO res = accountServices.RefreshToken(userId);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }
    }
}

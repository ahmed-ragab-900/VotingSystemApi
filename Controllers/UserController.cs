using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.User;
using VotingSystemApi.Services.Response;
using VotingSystemApi.Services.Users;

namespace VotingSystemApi.Controllers
{
    [ApiController, Authorize]
    public class UserController : BaseController
    {
        private readonly IUserServices userServices;

        public UserController(IUserServices userServices, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.userServices = userServices;
        }

        [Route("AllAuthorizedUsers"), HttpGet]
        public object AllAuthorizedUsers([FromQuery] Filter f)
        {
            try
            {
                var res = userServices.AllAuthorizedUsers(f);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("AllUnAuthorizedUsers"), HttpGet]
        public object AllUnAuthorizedUsers([FromQuery] Filter f)
        {
            try
            {
                var res = userServices.AllUnAthorizedUsers(f);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("AllWaitingUsers"), HttpGet]
        public object AllWaitingUsers([FromQuery] Filter f)
        {
            try
            {
                var res = userServices.AllWaitinUsers(f);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("UserById/{id}"), HttpGet]
        public object UserById(string id)
        {
            try
            {
                var res = userServices.UserById(id);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("Authorize/{id}"), HttpPut]
        public object Authorize(string id)
        {
            try
            {
                var res = userServices.Authorize(id);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("UnAuthorize/{id}"), HttpPut]
        public object UnAuthorize(string id)
        {
            try
            {
                var res = userServices.UnAuthorize(id);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("UpdateProfileImage/{id}"), HttpPut]
        public object UpdateProfileImage(string id, [FromBody] string base64)
        {
            try
            {
                var res = userServices.UpdateProfileImage(id, base64);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("UpdateUserData/{id}"), HttpPut]
        public object UpdateUserData(string id, [FromBody] UpdateUserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                var res = userServices.UpdateUserDate(id, dto);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("UpdatePassword/{id}"), HttpPut]
        public object UpdatePassword(string id, string newPassword)
        {
            try
            {
                var res = userServices.UpdatePassword(id, newPassword);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }
    }
}

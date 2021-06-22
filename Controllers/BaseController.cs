using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotingSystemApi.Helpers;

namespace VotingSystemApi.Controllers
{

    [ApiController]
    [Route("api/[controller]/")]
    public class BaseController : ControllerBase
    {

        private IHttpContextAccessor _httpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            if (Helper.serverPath == null)
            {
                var request = httpContextAccessor.HttpContext.Request;
                string serverPath = $"{request.Scheme}://{request.Host}{request.PathBase}";
                Helper.serverPath = serverPath + "/images/";
            }
        }


        public string LoggedInUserId
        { 
            get 
            {
                return _httpContextAccessor.HttpContext?.User?.Claims?.Where(c => c.Type == ClaimTypes.NameIdentifier)?.SingleOrDefault()?.Value;
            } 
        }

        public bool IsSuperAdmin
        {
            get
            {
                string userRole = _httpContextAccessor.HttpContext?.User?.Claims?.Where(c => c.Type == ClaimTypes.Role)?.SingleOrDefault()?.Value;
                if (userRole == "1")
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsAdmin
        {
            get
            {
                string userRole = _httpContextAccessor.HttpContext?.User?.Claims?.Where(c => c.Type == ClaimTypes.Role)?.SingleOrDefault()?.Value;
                if (userRole == "2")
                {
                    return true;
                }
                return false;
            }
        }

        public string LoggedInUserName { get { return _httpContextAccessor.HttpContext?.User?.Identity?.Name; } }
        public string ServerRootPath { get { return $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; } }
        public string IP { get { return _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString(); } }
    }
}
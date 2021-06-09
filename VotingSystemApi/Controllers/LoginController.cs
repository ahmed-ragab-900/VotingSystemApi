using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.Models;

namespace VotingSystemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public object Login(string name, string pass)
        {
            using (VotintSystemContext db = new Models.VotintSystemContext())  
            {
                Helper.Auth auth = new Helper.Auth();
                User user = db.Users.FirstOrDefault(p => p.UserName == name && p.Password == pass);
                return new
                {
                    token = auth.GenerateJSONWebToken(user.Id, user.UserName),
                    user = user
                };
            }
             
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;
using VotingSystemApi.Services.StudentUnion;

namespace VotingSystemApi.Controllers
{
    [ApiController, Authorize]
    public class StudentUnionController : BaseController
    {
        private readonly IStudentUnionServices _unionServices;
        public StudentUnionController(IHttpContextAccessor httpContextAccessor, IStudentUnionServices unionServices) : base(httpContextAccessor) 
        {
            _unionServices = unionServices;
        }
        // GET: api/<StudentUnionController>
        [HttpGet]
        public object StudentUonion()
        {
            try
            {
                var res = _unionServices.StudentUnion();
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        //// GET api/<StudentUnionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<StudentUnionController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<StudentUnionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<StudentUnionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

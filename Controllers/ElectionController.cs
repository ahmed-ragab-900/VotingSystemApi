using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Elections;
using VotingSystemApi.Services.Elections;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Controllers
{
    [Authorize]
    [ApiController]
    public class ElectionController : BaseController
    {
        private readonly IElectionServices electionServices;

        public ElectionController(IElectionServices electionServices, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            this.electionServices = electionServices;
        }
        
        [Route("CurrentElection"), HttpGet]
        public object CurrentElection()
        {
            try
            {
                if (IsSuperAdmin)
                {
                    var res = electionServices.CurrentElection();
                    return Ok(res);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("ElectionById/{id}"), HttpGet]
        public object ElectionById(string id)
        {
            try
            {
                if (IsSuperAdmin)
                {
                    var res = electionServices.ElectionById(id);
                    return Ok(res);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("AllElections"), HttpGet]
        public object AllElections([FromQuery]Filter f)
        {
            try
            {
                if (IsSuperAdmin)
                {
                    var res = electionServices.AllElections(f);
                    return Ok(res);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        //[Route("AllElectionsWithDetails"), HttpGet]
        //public object AllElectionsWithDetails([FromQuery] Filter f)
        //{
        //    try
        //    {
        //        if (IsSuperAdmin)
        //        {
        //            var res = electionServices.AllElectionsWithDetails(f);
        //            return Ok(res);
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ResponseServices.somethingRwong);
        //    }
        //}

        [Route("StartElection"), HttpPost]
        public object StartElection([FromBody] AddElectionDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                if (IsSuperAdmin)
                {
                    var res = electionServices.StartElection(dto);
                    return Ok(res);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("CancelElection/{id}"), HttpPut]
        public object CancelElection(string id)
        {
            try
            {
                if (IsSuperAdmin)
                {
                    var res = electionServices.CancelElection(id);
                    return Ok(res);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }
    }
}

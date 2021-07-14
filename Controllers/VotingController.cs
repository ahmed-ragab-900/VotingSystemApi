using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Vote;
using VotingSystemApi.Services.Response;
using VotingSystemApi.Services.Voting;

namespace VotingSystemApi.Controllers
{
    [ApiController ,Authorize]
    public class VotingController : BaseController
    {
        private readonly IVotingServices _votingServices;
        public VotingController(IVotingServices votingServices,IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _votingServices = votingServices;
        }

        [Route("Voting"), HttpPost]
        public object Voting([FromBody] VotingDTO dto)
        {
            try
            {
                var res = _votingServices.Voteing(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        //[Route("VotingToUser"), HttpGet]
        //public object VotingToUser([FromQuery] Filter f )
        //{
        //    try
        //    {
        //        var res = _votingServices.VotingToUser(f);
        //        return Ok(res);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ResponseServices.somethingRwong);
        //    }
        //}

        //[Route("UpdateVoting"), HttpPost]
        //public object UpdateVoting([FromBody] VoteDTO dto)
        //{
        //    try
        //    {
        //        var res = _votingServices.UpdateVoting(dto);
        //        return Ok(res);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ResponseServices.somethingRwong);
        //    }
        //}

        //[Route("CheckByRole"), HttpGet]
        //public object CheckByRole([FromQuery] Filter f)
        //{
        //    try
        //    {
        //        var res = _votingServices.CheckByRole(f);
        //        return Ok(res);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ResponseServices.somethingRwong);
        //    }
        //}
    }
}

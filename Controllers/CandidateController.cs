using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Candidates;
using VotingSystemApi.Services.Candidates;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Controllers
{
    [ApiController, Authorize]
    public class CandidateController : BaseController
    {
        private readonly ICandidateServices _candidateService;
        public CandidateController(ICandidateServices candidateServices, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _candidateService = candidateServices;
        }

        [Route("AllCandidates"), HttpGet]
        public object AllCandidates(string electionId, [FromQuery] Filter f)
        {
            try
            {
                var res = _candidateService.AllCandidates(electionId, f);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("WaitingCandidate"), HttpGet]
        public object WaitingCandidate(string electionId)
        {
            try
            {
                var res = _candidateService.WaitingCandidate(electionId);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("Commissions"), HttpGet]
        public object Commissions()
        {
            try
            {
                var res = _candidateService.Commissions();
                return Ok(res);
            }
            catch (Exception ed)
            {
                return BadRequest(ResponseServices.somethingRwong + $"\n {ed.Message}");
            }
        }

        [Route("AddCandidate"), HttpPost]
        public object AddCandidates([FromBody] AddCandidateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ResponseServices.incorrectModel);
                }
                var res = _candidateService.AddCandidate(dto);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("EditCandidate"), HttpPut]
        public object EditCandidates([FromBody] EditCandidateDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return BadRequest(ResponseServices.incorrectModel);
                }
                var res = _candidateService.EditCandidate(dto);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("DeleteCandidate/{Id}"), HttpDelete]
        public object DeleteCandidates(string id)
        {
            try
            {
                var res = _candidateService.DeleteCandidate(id);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("AcceptCandidate/{Id}"), HttpPut]
        public object AcceptCandidates(string id)
        {
            try
            {
                var res = _candidateService.AcceptCandidate(id);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("RefuseCandidate/{Id}"), HttpPut]
        public object RefuseCandidates(string id)
        {
            try
            {
                var res = _candidateService.RefuseCandidate(id);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("UserIsCandidate/{Id}"), HttpGet]
        public object UserIsCandidates(string id)
        {
            try
            {
                var res = _candidateService.UserIsCandidate(id);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }
    }
}

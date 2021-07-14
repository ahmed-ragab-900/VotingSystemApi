using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Complaints;
using VotingSystemApi.Services.Complaints;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Controllers
{
    [ApiController, Authorize]
    public class ComplaintController : BaseController
    {
        private readonly IComplaintServices complaintServices;

        public ComplaintController(IComplaintServices complaintServices ,IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            this.complaintServices = complaintServices;
        }

        [Route("AllSolvedComplaints"), HttpGet]
        public object AllSolvedComplaints([FromQuery] Filter f)
        {
            try
            {
                var res = complaintServices.AllSolvedComplaints(f);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("AllNotSolvedComplaints"), HttpGet]
        public object AllNotSolvedComplaints([FromQuery] Filter f)
        {
            try
            {
                var res = complaintServices.AllNotSolvedComplaints(f);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("ComplaintById/{id}"), HttpGet]
        public object ComplaintById(string id)
        {
            try
            {
                var res = complaintServices.ComplaintById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("AddComplaint"), HttpPost]
        public object AddComplaint([FromBody]AddComplaintDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                var res = complaintServices.AddComplaint(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("EditComplaint"), HttpPut]
        public object EditComplaint([FromBody] EditComplaintDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                var res = complaintServices.EditComplaint(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("Solved/{id}"), HttpPut]
        public object Solved(string id)
        {
            try
            {
                var res = complaintServices.Solved(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("DeleteComplaint/{id}"), HttpDelete]
        public object DeleteComplaint(string id)
        {
            try
            {
                var res = complaintServices.DeleteComplaint(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }
    }
}

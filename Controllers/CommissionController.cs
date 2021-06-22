using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Commissions;
using VotingSystemApi.Services.Commissions;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionController : BaseController
    {
        private readonly ICommissionServices commissionServices;
        public CommissionController(ICommissionServices commissionServices,IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.commissionServices = commissionServices;
        }

        [Route("AllCommission"), HttpGet]
        public object AllCommission([FromQuery] Filter f)
        {
            try
            {
                var res = commissionServices.AllCommission(f);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("CommissionById/{id}"), HttpGet]
        public object CommissionById(string id)
        {
            try
            {
                var res = commissionServices.CommissionById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("AddCommission"), HttpPost]
        public object AddCommission([FromBody]AddCommissionDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                var res = commissionServices.AddCommission(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("EditCommission/{id}"), HttpPut]
        public object EditCommission([FromBody]EditCommissionDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                var res = commissionServices.EditCommission(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("DeleteCommission/{id}"), HttpDelete]
        public object DeleteCommission(string id)
        {
            try
            {
                var res = commissionServices.DeleteCommission(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }
    }
}

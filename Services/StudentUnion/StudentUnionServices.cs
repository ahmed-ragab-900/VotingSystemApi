using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.StudentUnion;
using VotingSystemApi.Helpers;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Services.StudentUnion
{

    public class StudentUnionServices : IStudentUnionServices
    {
    private readonly IResponseServices responseServices;
        public StudentUnionServices(IResponseServices responseServices)
        {
            this.responseServices = responseServices;
        }
        public ResponseDTO StudentUnion()
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Election election = db.Elections.Where(p => (p.IsEnded == true || p.EndVoting < DateTime.Now) && p.IsCanceled != true)
                    .OrderByDescending(p => p.EndVoting)
                    .Include(p => p.Candidates).ThenInclude(p => p.Commission)
                    .Include(p => p.Candidates).ThenInclude(p => p.User)
                    .FirstOrDefault();


                StudentUnionDTO dto = new StudentUnionDTO()
                {
                    ElectionId = election.Id,
                    Year = election.Year,
                    Details = election.Candidates.GroupBy(p => p.CommissionId).Select(x => new CommssionsDTO()
                    {
                        CommissionId = x.FirstOrDefault().CommissionId,
                        CommissionName = x.FirstOrDefault().Commission.Name,
                        candidates = x.Where(p => p.IsPending != true && p.IsDeleted != true && p.IsRefused != true && p.IsAccepted == true).Select(u => new Member()
                        {
                            UserId = u.UserId,
                            UserName = u.User.Name,
                            UserImage = u.User.Image != null ? Helper.serverPath + u.User.Image : null,
                            UserYear = u.User.Year ?? 0,
                        }).ToList()
                    }).ToList()
                };

                return responseServices.passed(dto);
            }
        }
    }
}

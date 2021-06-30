using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Elections;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Services.Elections
{
    public class ElectionServices : IElectionServices
    {
        private readonly IResponseServices responseServices;
        private readonly IMapper mapper;
        public ElectionServices(IResponseServices responseServices, IMapper mapper)
        {
            this.responseServices = responseServices;
            this.mapper = mapper;
        }

        public ResponseDTO CurrentElection()
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Election election = db.Elections.Where(p => p.IsEnded != true && p.IsCanceled != true).OrderByDescending(p => p.EndVoting).FirstOrDefault();
                if(election != null)
                {
                    ElectionWithDetailsDTO electionDTO = mapper.Map<ElectionWithDetailsDTO>(election);
                    return responseServices.passed(electionDTO);
                }
                else
                {
                    return responseServices.passedWithMessage("No Election exist at this moment");
                }
            }
        }

        public ResponseDTO ElectionById(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Election election = db.Elections
                    .Include(o => o.Candidates).ThenInclude(o => o.Commission)
                    .Include(o => o.Candidates).ThenInclude(o => o.User)
                    .FirstOrDefault(p => p.Id == id);
                if (election != null)
                {
                    ElectionWithDetailsDTO electionDTO = mapper.Map<ElectionWithDetailsDTO>(election);
                    return responseServices.passed(electionDTO);
                }
                else
                {
                    return responseServices.passedWithMessage("No Election exist at this moment");
                }
            }
        }

        public ResponseDTO AllElections(Filter f)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var elections = db.Elections.OrderByDescending(p => p.EndVoting);
                if (f.SearchText != null && f.SearchText != "")
                {
                    elections = elections.Where(p => p.Year.ToString().Contains(f.SearchText)).OrderByDescending(p => p.EndVoting);
                }

                var currentElections = elections.OrderByDescending(p => p.EndVoting).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage);
                int electionsCount = elections.Count();
                PageOfData<ElectionDTO> output = new PageOfData<ElectionDTO>
                {
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)electionsCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    CurrentPageSize = electionsCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : electionsCount % f.ItemsPerPage,
                    AllItems = electionsCount,
                    PageSize = f.ItemsPerPage,
                    Result = mapper.Map<List<ElectionDTO>>(currentElections.ToList())
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO AllElectionsWithDetails(Filter f)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var elections = db.Elections.OrderByDescending(p => p.EndVoting);
                if (f.SearchText != null && f.SearchText != "")
                {
                    elections = elections.Where(p => p.Year.ToString().Contains(f.SearchText)).OrderByDescending(p => p.EndVoting);
                }

                var currentElections = elections.OrderByDescending(p => p.EndVoting).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage)
                    .Include(o => o.Candidates).ThenInclude(o => o.Commission)
                    .Include(o => o.Candidates).ThenInclude(o => o.User);
                int electionsCount = elections.Count();

                PageOfData<ElectionDTO> output = new PageOfData<ElectionDTO>
                {
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)electionsCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    CurrentPageSize = electionsCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : electionsCount % f.ItemsPerPage,
                    AllItems = electionsCount,
                    PageSize = f.ItemsPerPage,
                    Result = mapper.Map<List<ElectionDTO>>(currentElections.ToList())
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO StartElection(AddElectionDTO dto)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Election election = mapper.Map<Election>(dto);
                election.Id = Guid.NewGuid().ToString();
                election.IsCanceled = false;
                election.IsEnded = false;
                election.AllVoters = db.Users.Where(p => p.RoleId == 3 && p.IsAuthorized == true).Count();
                election.Voted = 0;
                election.NotVoted = 0;
                db.Elections.Add(election);
                if(db.SaveChanges() > 0)
                {
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed(ResponseServices.somethingRwong);
                }
            }
        }

        public ResponseDTO CancelElection(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Election election = db.Elections.FirstOrDefault(p => p.Id == id);
                if(election != null)
                {
                    election.IsCanceled = true;
                    election.IsEnded = false;
                    election.CancelDate = DateTime.Now;
                    db.SaveChanges();
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed("This Election doesn't exist in database");
                }
            }
        }
    }
}

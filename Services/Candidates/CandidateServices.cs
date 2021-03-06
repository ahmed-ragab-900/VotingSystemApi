using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Candidates;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Services.Candidates
{
    public class CandidateServices : ICandidateServices
    {
        private readonly IResponseServices responseServices;
        private readonly IMapper mapper;
        public CandidateServices(IResponseServices responseServices, IMapper mapper)
        {
            this.responseServices = responseServices;
            this.mapper = mapper;
        }

        public ResponseDTO AllCandidates(string electionId, Filter f)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var candidates = db.Candidates.Where(p => p.IsDeleted != true);
                if (f.SearchText != null && f.SearchText != "")
                {
                    string search = f.SearchText.ToLower();
                    candidates = candidates.Where(p => p.Commission.Name.Contains(search) || p.User.Name.Contains(search) || p.User.Year.ToString() == search);
                }

                var currentCandidates = candidates.OrderByDescending(p => p.Date).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage).Include(o => o.User).Include(o => o.Commission);
                int candidatesCount = candidates.Count();

                PageOfData<CandidateDTO> output = new PageOfData<CandidateDTO>
                {
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)candidatesCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    CurrentPageSize = candidatesCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : candidatesCount % f.ItemsPerPage,
                    AllItems = candidatesCount,
                    PageSize = f.ItemsPerPage,
                    Result = mapper.Map<List<CandidateDTO>>(currentCandidates.ToList())
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO WaitingCandidate(string electionId)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var candidates = db.Candidates.Where(p => p.IsDeleted != true && p.ElectionId == electionId && p.IsPending == true);
                var currentCandidates = candidates.OrderByDescending(p => p.Date).Include(o => o.User).Include(o => o.Commission);
                var output = mapper.Map<List<CandidateDTO>>(currentCandidates.ToList());
                return responseServices.passed(output);
            }
        }

        public ResponseDTO AddCandidate(AddCandidateDTO dto)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                dto.Id = Guid.NewGuid().ToString();
                Candidate candidate = mapper.Map<Candidate>(dto);
                candidate.Date = DateTime.Now;
                db.Candidates.Add(candidate);
                if (db.SaveChanges() > 0)
                {
                    Candidate newCandidate = db.Candidates.Where(p => p.Id == candidate.Id).Include(p => p.User).Include(p => p.Commission).FirstOrDefault();
                    return responseServices.passed(mapper.Map<CandidateDTO>(newCandidate));
                }
                else
                {
                    return responseServices.failed(ResponseServices.somethingRwong);
                }
            }
        }

        public ResponseDTO EditCandidate(EditCandidateDTO dto)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Candidate candidate = db.Candidates.FirstOrDefault(p => p.Id == dto.Id);
                if (candidate != null)
                {
                    candidate.CommissionId = dto.CommissionId;
                    db.SaveChanges();
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed("This Candidate doesn't exist in database");
                }
            }
        }

        public ResponseDTO DeleteCandidate(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Candidate candidate = db.Candidates.FirstOrDefault(p => p.Id == id);
                if (candidate != null)
                {
                    candidate.IsDeleted = true;
                    db.SaveChanges();
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed("This Candidate doesn't exist in database");
                }
            }
        }

        public ResponseDTO Commissions()
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var commissions = db.Commissions.Where(p => p.IsDeleted != true).Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList();
                return responseServices.passed(commissions);
            }
        }

        public ResponseDTO AcceptCandidate(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Candidate candidate = db.Candidates.FirstOrDefault(p => p.Id == id);
                if (candidate != null)
                {
                    candidate.IsPending = false;
                    candidate.IsAccepted = true;
                    candidate.IsRefused = false;
                    db.SaveChanges();
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed("This Candidate doesn't exist in database");
                }
            }
        }

        public ResponseDTO RefuseCandidate(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Candidate candidate = db.Candidates.FirstOrDefault(p => p.Id == id);
                if (candidate != null)
                {
                    candidate.IsPending = false;
                    candidate.IsAccepted = false;
                    candidate.IsRefused = true;
                    db.SaveChanges();
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed("This Candidate doesn't exist in database");
                }
            }
        }

        public ResponseDTO UserIsCandidate(string userId)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Election currentElection = db.Elections.OrderByDescending(p => p.EndRequests).Where(p => p.IsCanceled != true && p.IsEnded != true).FirstOrDefault();
                if (currentElection != null)
                {
                    bool exist = db.Candidates.Any(p => p.ElectionId == currentElection.Id && p.UserId == userId);
                    if (exist)
                    {
                        return responseServices.passed("Yes");
                    }
                    else
                    {
                        return responseServices.passed("No");
                    }
                }
                else
                {
                    return responseServices.passed("No");
                }

            }
        }
    }
}

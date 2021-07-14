using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.VoteDTO;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;
using Microsoft.AspNetCore.Identity;

namespace VotingSystemApi.Services.Voting
{
    public class VotingServices : IVotingServices
    {
        private readonly IResponseServices _responseServices;
        private readonly IMapper _mapper;
        public VotingServices(IResponseServices responseServices , IMapper mapper)
        {
            _responseServices = responseServices;
            _mapper = mapper;
        }
        public ResponseDTO VotingToUser(Filter f)
        {
            using (VotingSystemContext db = new VotingSystemContext()) 
            {
                var votes = db.Votes
                             .Include(z => z.Commission)
                             .Include(z => z.User.Candidates)
                             .Where(z=>z.Date.Year == DateTime.Now.Year);


                var currentvote = votes.Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage)
                  .Include(o => o.Election.Candidates).ThenInclude(o => o.Commission)
                  .Include(o => o.Election.Candidates).ThenInclude(o => o.User);
                votes = votes.OrderByDescending(z => z.Id);
                int voteCount = votes.Count();
                PageOfData<VoteDTO> output = new PageOfData<VoteDTO>
                {
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)voteCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    CurrentPageSize = voteCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : voteCount % f.ItemsPerPage,
                    AllItems = voteCount,
                    PageSize = f.ItemsPerPage,
                    Result = _mapper.Map<List<VoteDTO>>(currentvote.ToList())
                };
                return _responseServices.passed(output);

            }
        }
        public ResponseDTO UpdateVoting(VoteDTO dto)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var vote = db.Votes
                                   .Include(x=>x.CommissionId)
                                   .Include(x=>x.Election.Candidates)
                                   .FirstOrDefault(x=>x.Id == dto.Id);
                if (vote != null)
                {
                    VoteDTO votedto = _mapper.Map<VoteDTO>(vote);
                   return _responseServices.passed(votedto);

                }
                else
                {
                  return  _responseServices.passedWithMessage("Can't Complete your Process");
                }
                
            }
        }

    }
}

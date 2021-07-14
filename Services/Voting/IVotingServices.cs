using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Vote;

namespace VotingSystemApi.Services.Voting
{
    public interface IVotingServices
    {
        public ResponseDTO Voteing(VotingDTO dto);
        //public ResponseDTO VotingToUser(Filter f);
        //public ResponseDTO UpdateVoting(VoteDTO dto);
    }
}

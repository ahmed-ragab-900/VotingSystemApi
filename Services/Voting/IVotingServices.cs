using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.VoteDTO;

namespace VotingSystemApi.Services.Voting
{
    public interface IVotingServices
    {
        public ResponseDTO VotingToUser(Filter f);
        public ResponseDTO UpdateVoting(VoteDTO dto);
        public ResponseDTO CheckByRole(Filter f);
    }
}

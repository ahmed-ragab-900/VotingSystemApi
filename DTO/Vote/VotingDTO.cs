using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.Vote
{
    public class VotingDTO
    {
        public string VoterId { get; set; }
        public string ElectionId { get; set; }
        public List<Voteing> votes { get; set; }
    }

    public class Voteing
    {
        public string UserId { get; set; }
        public string CommissionId { get; set; }
    }
}

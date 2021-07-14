using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.VoteDTO
{
    public class VoteDTO
    {
        public string Id { get; set; }
        public string VoterId { get; set; }
        public string UserId { get; set; }
        public string CommissionId { get; set; }
        public string ElectionId { get; set; }
        public DateTime Date { get; set; }
        //UI
        public string UserName { get; set; }
        public string CommissionName { get; set; }

    }
}

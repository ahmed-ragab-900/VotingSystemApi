using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.Elections
{
    public class ElectionDTO
    {
        public string Id { get; set; }
        public DateTime StartRequests { get; set; }
        public DateTime EndRequests { get; set; }
        public DateTime StartVoting { get; set; }
        public DateTime EndVoting { get; set; }
        public int Year { get; set; }
        public bool? IsEnded { get; set; }
        public int? AllVoters { get; set; }
        public int? Voted { get; set; }
        public int? NotVoted { get; set; }
        public DateTime? CancelDate { get; set; }
        public bool? IsCanceled { get; set; }
    }
}

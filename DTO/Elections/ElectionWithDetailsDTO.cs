using System;
using System.Collections.Generic;

namespace VotingSystemApi.DTO.Elections
{
    public class ElectionWithDetailsDTO
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
        public List<ElectionCommssionsDTO> Details { get; set; }
    }
}

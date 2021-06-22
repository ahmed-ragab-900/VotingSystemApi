using System;

namespace VotingSystemApi.DTO.Elections
{
    public class AddElectionDTO
    {
        public DateTime StartRequests { get; set; }
        public DateTime EndRequests { get; set; }
        public DateTime StartVoting { get; set; }
        public DateTime EndVoting { get; set; }
        public int Year { get; set; }
    }
}

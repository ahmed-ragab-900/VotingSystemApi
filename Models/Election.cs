using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class Election
    {
        public Election()
        {
            Candidates = new HashSet<Candidate>();
            StudentUnions = new HashSet<StudentUnion>();
            Votes = new HashSet<Vote>();
        }

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

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<StudentUnion> StudentUnions { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}

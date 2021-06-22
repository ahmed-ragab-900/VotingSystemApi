using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class Commission
    {
        public Commission()
        {
            Candidates = new HashSet<Candidate>();
            StudentUnions = new HashSet<StudentUnion>();
            Votes = new HashSet<Vote>();
        }

        public string Id { get; set; }
        public int? No { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<StudentUnion> StudentUnions { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}

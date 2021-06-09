using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class Vote
    {
        public string Id { get; set; }
        public string VoterId { get; set; }
        public string UserId { get; set; }
        public string CommissionId { get; set; }
        public string ElectionId { get; set; }
        public DateTime Date { get; set; }

        public virtual Commission Commission { get; set; }
        public virtual Election Election { get; set; }
        public virtual User User { get; set; }
        public virtual User Voter { get; set; }
    }
}

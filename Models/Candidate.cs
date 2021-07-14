using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class Candidate
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CommissionId { get; set; }
        public string ElectionId { get; set; }
        public DateTime Date { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsRefused { get; set; }
        public bool IsPending { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? Status { get; set; }

        public virtual Commission Commission { get; set; }
        public virtual Election Election { get; set; }
        public virtual User User { get; set; }
    }
}

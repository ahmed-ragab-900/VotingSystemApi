using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class StudentUnion
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CommissionId { get; set; }
        public bool? IsPresident { get; set; }
        public bool? IsAssistant { get; set; }
        public string ElectionId { get; set; }

        public virtual Commission Commission { get; set; }
        public virtual Election Election { get; set; }
        public virtual User User { get; set; }
    }
}

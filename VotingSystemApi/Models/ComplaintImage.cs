using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class ComplaintImage
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string ComplaintId { get; set; }

        public virtual Complaint Complaint { get; set; }
    }
}

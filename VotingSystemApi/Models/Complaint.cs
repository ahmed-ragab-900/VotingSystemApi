using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class Complaint
    {
        public Complaint()
        {
            ComplaintImages = new HashSet<ComplaintImage>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime? Date { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ComplaintImage> ComplaintImages { get; set; }
    }
}

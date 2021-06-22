using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class PostComment
    {
        public string Id { get; set; }
        public string Comment { get; set; }
        public string PostId { get; set; }
        public DateTime? Date { get; set; }
        public string UserId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}

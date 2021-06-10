using System;
using System.Collections.Generic;

#nullable disable

namespace VotingSystemApi.Models
{
    public partial class PostImage
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}

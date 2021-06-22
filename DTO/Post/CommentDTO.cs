using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.Post
{
    public class CommentDTO
    {
        public string Id { get; set; }
        public string Comment { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
    }
}

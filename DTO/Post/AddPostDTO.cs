using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.Post
{
    public class AddPostDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public List<string> images { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.Post
{
    public class EditPostDTO
    {
        public string Text { get; set; }
        public List<string> images { get; set; }
    }
}

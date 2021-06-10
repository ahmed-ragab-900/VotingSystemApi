using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.Body
{
    public class PostSignInDTO
    {
        public string username { set; get; }
        public string password { set; get; }
    }
}

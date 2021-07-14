using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.StudentUnion
{
    public class StudentUnionDTO
    {
        public string ElectionId { get; set; }
        public int Year { get; set; }
        public List<CommssionsDTO> Details { get; set; }
    }

    public class CommssionsDTO
    {
        public string CommissionId { get; set; }
        public string CommissionName { get; set; }
        public List<Member> candidates { get; set; }
    }

    public class Member
    {
        public string UserId { get; set; }
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public int UserYear { get; set; }
    }
}

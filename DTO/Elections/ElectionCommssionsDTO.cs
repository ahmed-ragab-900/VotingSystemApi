using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.Elections
{
    public class ElectionCommssionsDTO
    {
        public string CommissionId { get; set; }
        public string CommissionName { get; set; }
        public List<CandidateDetailsDTO> candidates { get; set; }
    }

    public class CandidateDetailsDTO
    {
        public string UserId { get; set; }
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public int votes { get; set; }
    }
}

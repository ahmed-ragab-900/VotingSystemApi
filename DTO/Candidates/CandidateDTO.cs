using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.Candidates
{
    public class CandidateDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string UserYear { get; set; }
        public string UserAcademicNo { get; set; }
        public string CommissionId { get; set; }
        public string CommissionName { get; set; }
        public string ElectionId { get; set; }
        public DateTime Date { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsRefused { get; set; }
        public bool IsPending { get; set; }
    }
}

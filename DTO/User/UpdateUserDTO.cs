using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.User
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string AcademicNumber { get; set; }
        public string IdentityId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public int Year { get; set; }
    }
}

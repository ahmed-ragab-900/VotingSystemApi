using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.Body
{
    public class PostSignUpDTO
    {
        public string Name { get; set; }
        public string AcademicNumber { get; set; }
        public string IdentityId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Base64Image { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public int Year { get; set; }
    }
}

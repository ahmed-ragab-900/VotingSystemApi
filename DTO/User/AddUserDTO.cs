using System;

namespace VotingSystemApi.DTO.User
{
    public class AddUserDTO
    {
        public string Name { get; set; }
        public string AcademicNumber { get; set; }
        public string IdentityId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Base64Image { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public int Year { get; set; }
    }
}

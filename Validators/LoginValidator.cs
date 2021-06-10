using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystemApi.DTO.Body;
using VotingSystemApi.Models;

namespace VotingSystemApi.Validators
{
    public class LoginValidator
    {
        public bool SignInValidator(PostSignInDTO dto)
        {
            using (VotintSystemContext db = new VotintSystemContext())
            {
                if (string.IsNullOrEmpty(dto.username) || string.IsNullOrEmpty(dto.password))
                {
                    return false;
                }
                else
                {
                    return db.Users.Any(p => (p.UserName == dto.username || p.Email == dto.username || p.AcademicNumber == dto.username) && p.Password == dto.password);
                }
            }
        }
    }
}

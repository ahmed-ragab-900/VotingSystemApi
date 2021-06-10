using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Body;
using VotingSystemApi.Models;
using VotingSystemApi.Validators;

namespace VotingSystemApi.Services
{
    public class LoginServices
    {
        private VotintSystemContext db = new VotintSystemContext();
        private ResponseServices responseServices = new ResponseServices(); 
        
        public ResponseDTO SignIn(PostSignInDTO dto)
        {
            Helper.Auth auth = new Helper.Auth();
            LoginValidator validator = new LoginValidator();
            
            if (validator.SignInValidator(dto))
            {
                User user = GetUser(dto.username, dto.password);
                return responseServices.passed(new
                {
                    token = auth.GenerateJSONWebToken(user.Id, user.RoleId, user.Name, user.AcademicNumber),
                    user = user
                });
            }
            else
            {
                return responseServices.failed("Incorrect username or password");
            }
        }

        //public ResponseDTO SignUp(PostSignUpDTO dto)
        //{
        //    Helper.Helper helper = new Helper.Helper();
        //    User user = helper.AutoMap<PostSignUpDTO, User>(dto);
        //    using (VotintSystemContext db = new VotintSystemContext())
        //    {
        //        user.Id = Guid.NewGuid().ToString();
        //        user.No = Convert.ToInt32(db.Users.Max(p => p.No)) + 1;

        //    }

        //}

        private User GetUser(string username, string password, string id = null)
        {
            if (id == null)
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    return db.Users.FirstOrDefault(p => (p.UserName == username || p.Email == username || p.AcademicNumber == username) && p.Password == password);
                }
            }
            else
            {
                return db.Users.FirstOrDefault(p => p.Id == id);
            }
        }
    }
}

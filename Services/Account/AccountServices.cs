using AutoMapper;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.User;
using VotingSystemApi.Helpers;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;
using VotingSystemApi.Validators;

namespace VotingSystemApi.Services.Account
{
    public class AccountServices : IAccountServices
    {
        private readonly Auth auth = new Auth();
        private readonly Helper helper = new Helper();

        private readonly IResponseServices responseServices;
        private readonly IMapper mapper;

        public AccountServices(IResponseServices responseServices, IMapper mapper) 
        {
            this.responseServices = responseServices;
            this.mapper = mapper;
        }

        public ResponseDTO SignIn(string serverPath, SignInDTO dto)
        {
            VotingSystemContext db = new VotingSystemContext();
            AccountValidator validator = new AccountValidator();
            
            if (validator.SignInValidator(dto))
            {
                User user = GetUser(dto.username, dto.password);
                if (user.IsAuthorized == true)
                {
                    string token = auth.GenerateJSONWebToken(user.Id, user.RoleId, user.Name, user.AcademicNumber);
                    return responseServices.passed(new
                    {
                        token = token,
                        electionId = db.Elections.Where(p => p.IsEnded != true && p.IsCanceled != true && p.EndVoting >= DateTime.Now).OrderByDescending(p => p.EndVoting).FirstOrDefault()?.Id,
                        user = mapper.Map<UserDTO>(user)
                    });
                }
                else
                {
                    return responseServices.passedWithMessage("You are not authorized");
                }
            }
            else
            {
                return responseServices.failed("Incorrect username or password");
            }
        }

        public ResponseDTO SignUp(AddUserDTO dto)
        {
            User user = mapper.Map<User>(dto);
            using (VotingSystemContext db = new VotingSystemContext())
            {
                user.Id = Guid.NewGuid().ToString();
                user.No = Convert.ToInt32(db.Users.Max(p => p.No)) + 1;
                user.RoleId = 3;
                if(dto.Base64Image != null)
                    user.Image = helper.SaveBase64(dto.Base64Image);
                db.Users.Add(user);
                db.SaveChanges();
            }
            return responseServices.passed(new
            {
                token = auth.GenerateJSONWebToken(user.Id, user.RoleId, user.Name, user.AcademicNumber),
                user = mapper.Map<UserDTO>(user)
            });
        }

        public ResponseDTO RefreshToken(string userId)
        {
            User user = GetUser(null, null, userId);
            if(user != null)
            {
                Auth auth = new Auth();
                string newToken = auth.GenerateJSONWebToken(user.Id, user.RoleId, user.Name, user.AcademicNumber);
                return responseServices.passed(newToken);
            }
            else
            {
                return responseServices.failed(ResponseServices.somethingRwong);
            }
        }

        private User GetUser(string username, string password, string id = null)
        {
            using (VotingSystemContext db = new VotingSystemContext())
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
}

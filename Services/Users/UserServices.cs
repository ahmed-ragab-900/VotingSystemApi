using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.User;
using VotingSystemApi.Helpers;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly IResponseServices responseServices;
        private readonly IMapper mapper;

        public UserServices(IMapper mapper, IResponseServices responseServices)
        {
            this.mapper = mapper;
            this.responseServices = responseServices;
        }

        public ResponseDTO AllAuthorizedUsers(Filter f)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var users = db.Users.Where(p => p.IsAuthorized == true);
                if (f.SearchText != null && f.SearchText != "")
                {
                    users = users.Where(p => p.Name.Contains(f.SearchText) || p.AcademicNumber.Equals(f.SearchText));
                }

                var currentUsers = users.OrderByDescending(p => p.AcademicNumber).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage);
                int usersCount = users.Count();
                PageOfData<UserDTO> output = new PageOfData<UserDTO>
                {
                    AllItems = usersCount,
                    PageSize = f.ItemsPerPage,
                    CurrentPageSize = usersCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : usersCount % f.ItemsPerPage,
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)usersCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    Result = mapper.Map<List<UserDTO>>(currentUsers.ToList())
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO AllUnAthorizedUsers(Filter f)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var users = db.Users.Where(p => p.IsAuthorized == false);
                if (f.SearchText != null && f.SearchText != "")
                {
                    users = users.Where(p => p.Name.Contains(f.SearchText) || p.AcademicNumber.Equals(f.SearchText));
                }

                var currentUsers = users.OrderByDescending(p => p.AcademicNumber).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage);
                int usersCount = users.Count();
                PageOfData<UserDTO> output = new PageOfData<UserDTO>
                {
                    AllItems = usersCount,
                    PageSize = f.ItemsPerPage,
                    CurrentPageSize = usersCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : usersCount % f.ItemsPerPage,
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)usersCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    Result = mapper.Map<List<UserDTO>>(currentUsers.ToList())
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO AllWaitinUsers(Filter f)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var users = db.Users.Where(p => p.IsAuthorized == null);
                if (f.SearchText != null && f.SearchText != "")
                {
                    users = users.Where(p => p.Name.Contains(f.SearchText) || p.AcademicNumber.Equals(f.SearchText));
                }

                var currentUsers = users.OrderByDescending(p => p.AcademicNumber).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage);
                int usersCount = users.Count();
                PageOfData<UserDTO> output = new PageOfData<UserDTO>
                {
                    AllItems = usersCount,
                    PageSize = f.ItemsPerPage,
                    CurrentPageSize = usersCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : usersCount % f.ItemsPerPage,
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)usersCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    Result = mapper.Map<List<UserDTO>>(currentUsers.ToList())
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO UserById(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                User user = db.Users.FirstOrDefault(p => p.Id == id);
                if (user != null)
                {
                    return responseServices.passed(new
                    {
                        user = mapper.Map<UserDTO>(user)
                    });
                }
                else
                {
                    return responseServices.failed("This user doesn't exist in database");
                }
            }
        }

        public ResponseDTO Authorize(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                User user = db.Users.FirstOrDefault(p => p.Id == id);
                if (user != null)
                {
                    user.IsAuthorized = true;
                    if (db.SaveChanges() > 0)
                    {
                        return responseServices.passedWithMessage("You became Authoruized");
                    }
                    else
                    {
                        return responseServices.failed(ResponseServices.somethingRwong);
                    }
                }
                else
                {
                    return responseServices.failed("This user doesn't exist in database");
                }
            }
        }

        public ResponseDTO UnAuthorize(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                User user = db.Users.FirstOrDefault(p => p.Id == id);
                if (user != null)
                {
                    user.IsAuthorized = false;
                    if (db.SaveChanges() > 0)
                    {
                        return responseServices.passedWithMessage("You became Not Authoruized");
                    }
                    else
                    {
                        return responseServices.failed(ResponseServices.somethingRwong);
                    }
                }
                else
                {
                    return responseServices.failed("This user doesn't exist in database");
                }
            }
        }

        public ResponseDTO UpdateProfileImage(string id, string base64)
        {
            Helper helper = new Helper();
            using (VotingSystemContext db = new VotingSystemContext())
            {
                User user = db.Users.FirstOrDefault(p => p.Id == id);
                if (user != null)
                {
                    if(user.Image != null)
                        helper.deleteImage(user.Image);

                    user.Image = helper.SaveBase64(base64);
                    if (db.SaveChanges() > 0)
                    {
                        return responseServices.passed(Helper.serverPath + user.Image);
                    }
                    else
                    {
                        return responseServices.failed(ResponseServices.somethingRwong);
                    }
                }
                else
                {
                    return responseServices.failed("This user doesn't exist in database");
                }
            }
        }

        public ResponseDTO UpdateUserDate(string id, UpdateUserDTO dto)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                User user = db.Users.FirstOrDefault(p => p.Id == id);
                if (user != null)
                {
                    try
                    {
                        user.AcademicNumber = dto.AcademicNumber;
                        user.Name = dto.Name;
                        user.Address = dto.Address;
                        user.BirthDate = dto.BirthDate;
                        user.IdentityId = dto.IdentityId;
                        user.Mobile = dto.Mobile;
                        user.Year = dto.Year;
                        db.SaveChanges();

                        UserDTO u = mapper.Map<UserDTO>(user);
                        return responseServices.passed(u);
                    }
                    catch
                    {
                        return responseServices.failed(ResponseServices.somethingRwong);
                    }
                }
                else
                {
                    return responseServices.failed("This user doesn't exist in database");
                }
            }
        }
        
        public ResponseDTO UpdatePassword(string id, string newPassword)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                User user = db.Users.FirstOrDefault(p => p.Id == id);
                if (user != null)
                {
                    try
                    {
                        user.Password = newPassword;
                        db.SaveChanges();

                        UserDTO u = mapper.Map<UserDTO>(user);
                        return responseServices.passed(u);
                    }
                    catch
                    {
                        return responseServices.failed(ResponseServices.somethingRwong);
                    }
                }
                else
                {
                    return responseServices.failed("This user doesn't exist in database");
                }
            }
        }
    }
}

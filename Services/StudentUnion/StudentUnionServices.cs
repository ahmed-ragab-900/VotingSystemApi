using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.StudentUnion;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Services.StudentUnion
{

    public class StudentUnionServices : IStudentUnionServices
    {
        private readonly IResponseServices _responseServices;
        private readonly IMapper _mapper;
        public StudentUnionServices(IResponseServices responseServices , IMapper  mapper)
        {
            _responseServices = responseServices;
            _mapper = mapper;
        }
        public ResponseDTO DeleteItem(string id)
        {
            try
            {
                using (VotingSystemContext db = new VotingSystemContext())
                {
                    var studentUnion = db.StudentUnions.FirstOrDefault(p => p.Id == id);
                    if (studentUnion != null)
                    {
                        studentUnion.IsDeleted = true;
                        return _responseServices.passed(ResponseServices.Deleted);
                    }
                    else
                    {
                        return _responseServices.failed("This complaint doesn't exist in database");
                    }
                }

            }
            catch (Exception ex)
            {

                return _responseServices.passedWithMessage("Error" + ex.Message);
            }
        }

        public ResponseDTO GetItemById(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var  studentUnion = db.StudentUnions
                    .Include(o => o.User.Candidates).ThenInclude(o => o.Commission)
                    .Include(o => o.User.Candidates).ThenInclude(o => o.User)
                    .FirstOrDefault(p => p.Id == id);
                if (studentUnion != null)
                {
                    var StudentUnionDTO = _mapper.Map<StudentUnionsDTO>(studentUnion);
                    return _responseServices.passed(StudentUnionDTO);
                }
                else
                {
                    return _responseServices.passedWithMessage("No Item doesn't exist");
                }
            }
        }
    }
}

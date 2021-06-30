using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Commissions;
using VotingSystemApi.Helpers;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Services.Commissions
{
    public class CommissionServices : ICommissionServices
    {
        private readonly IResponseServices responseServices;
        private readonly IMapper mapper;
        public CommissionServices(IResponseServices responseServices, IMapper mapper)
        {
            this.responseServices = responseServices;
            this.mapper = mapper;
        }

        public ResponseDTO AllCommission(Filter f)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var commissions = db.Commissions.Where(p => p.IsDeleted != true);
                if (f.SearchText != null && f.SearchText != "")
                {
                    string search = f.SearchText.ToLower();
                    commissions = commissions.Where(p => p.Name.ToLower().Contains(search) || p.Description.ToLower().Contains(search));
                }

                var currentCommissions = commissions.OrderByDescending(p => p.No).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage);
                int CommissionsCount = commissions.Count();
                
                PageOfData<CommissionDTO> output = new PageOfData<CommissionDTO>
                {
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)CommissionsCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    CurrentPageSize = CommissionsCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : CommissionsCount % f.ItemsPerPage,
                    AllItems = CommissionsCount,
                    PageSize = f.ItemsPerPage,
                    Result = mapper.Map<List<CommissionDTO>>(currentCommissions.ToList())
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO CommissionById(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Commission commission = db.Commissions.FirstOrDefault(p => p.Id == id);
                if(commission != null)
                {
                    CommissionDTO comm = mapper.Map<CommissionDTO>(commission);
                    return responseServices.passed(comm);
                }
                else
                {
                    return responseServices.failed("This Commission doesn't exist in database");
                }
            }
        }

        public ResponseDTO AddCommission(AddCommissionDTO dto)
        { 
            Helper helper = new Helper();
            using (VotingSystemContext db = new VotingSystemContext())
            {
                dto.Id = Guid.NewGuid().ToString();
                if(dto.Image != null || dto.Image != "")
                {
                    dto.Image = helper.SaveBase64(dto.Image);
                }
                Commission commission = mapper.Map<Commission>(dto);
                db.Commissions.Add(commission);
                if(db.SaveChanges() > 0)
                {
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed(ResponseServices.somethingRwong);
                }
            }
        }

        public ResponseDTO EditCommission(EditCommissionDTO dto)
        {
            Helper helper = new Helper();
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Commission commission = db.Commissions.FirstOrDefault(p => p.Id == dto.Id);
                if(commission != null)
                {
                    commission.Name = dto.Name;
                    commission.Description = dto.Description;
                    if(dto.Image != null || dto.Image != null)
                    {
                        commission.Image = helper.SaveBase64(dto.Image);
                    }
                    else if(commission.Image != null)
                    {
                        helper.deleteImage(commission.Image);
                    }
                    db.SaveChanges();
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed(ResponseServices.somethingRwong);
                }
            }
        }

        public ResponseDTO DeleteCommission(string id)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Commission commission = db.Commissions.FirstOrDefault(p => p.Id == id);
                if (commission != null)
                {
                    commission.IsDeleted = true;
                    db.SaveChanges();
                    return responseServices.passedWithMessage(ResponseServices.Deleted);
                }
                else
                {
                    return responseServices.failed("This Commission doesn't exist in database");
                }
            }
        }
    }
}

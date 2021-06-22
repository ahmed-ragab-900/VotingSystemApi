using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Complaints;
using VotingSystemApi.Helpers;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Services.Complaints
{
    public class ComplaintServices : IComplaintServices
    {
        private readonly IResponseServices responseServices;
        private readonly IMapper mapper;
        public ComplaintServices(IResponseServices responseServices, IMapper mapper)
        {
            this.responseServices = responseServices;
            this.mapper = mapper;
        }

        public ResponseDTO AllSolvedComplaints(Filter f)
        {
            using (VotintSystemContext db = new VotintSystemContext())
            {
                var complaints = db.Complaints.Where(p => p.IsDeleted != true && p.IsSolved == true);
                if (f.SearchText != null && f.SearchText != "")
                {
                    string search = f.SearchText.ToLower();
                    complaints = complaints.Where(p => p.Title.ToLower().Contains(search) || p.User.Name.ToLower().Contains(search));
                }

                var currentComplaints = complaints.OrderByDescending(p => p.Date).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage).Include(o => o.User).Include(o => o.ComplaintImages);
                int complaintsCount = complaints.Count();
                
                PageOfData<ComplaintDTO> output = new PageOfData<ComplaintDTO>
                {
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)complaintsCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    CurrentPageSize = complaintsCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : complaintsCount % f.ItemsPerPage,
                    AllItems = complaintsCount,
                    PageSize = f.ItemsPerPage,
                    Result = mapper.Map<List<ComplaintDTO>>(currentComplaints.ToList())
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO AllNotSolvedComplaints(Filter f)
        {
            using (VotintSystemContext db = new VotintSystemContext())
            {
                var complaints = db.Complaints.Where(p => p.IsDeleted != true && p.IsSolved != true);
                if (f.SearchText != null && f.SearchText != "")
                {
                    string search = f.SearchText.ToLower();
                    complaints = complaints.Where(p => p.Title.ToLower().Contains(search) || p.User.Name.ToLower().Contains(search));
                }

                var currentComplaints = complaints.OrderByDescending(p => p.Date).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage).Include(o => o.User).Include(o => o.ComplaintImages);
                int complaintsCount = complaints.Count();

                PageOfData<ComplaintDTO> output = new PageOfData<ComplaintDTO>
                {
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)complaintsCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    CurrentPageSize = complaintsCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : complaintsCount % f.ItemsPerPage,
                    AllItems = complaintsCount,
                    PageSize = f.ItemsPerPage,
                    Result = mapper.Map<List<ComplaintDTO>>(currentComplaints.ToList())
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO ComplaintById(string id)
        {
            using (VotintSystemContext db = new VotintSystemContext())
            {
                var complaint = db.Complaints.Include(o => o.User).Include(o => o.ComplaintImages).FirstOrDefault(p => p.Id == id);
                if(complaint != null)
                {
                    ComplaintDTO comp = mapper.Map<ComplaintDTO>(complaint);
                    return responseServices.passed(comp);
                }
                else
                {
                    return responseServices.failed("This complaint doesn't exist in database");
                }
            }
        }

        public ResponseDTO AddComplaint(AddComplaintDTO dto)
        {
            Helper helper = new Helper();
            using (VotintSystemContext db = new VotintSystemContext())
            {
                dto.Id = Guid.NewGuid().ToString();
                dto.Date = DateTime.Now;
                Complaint complaint = mapper.Map<Complaint>(dto);
                db.Complaints.Add(complaint);
                if(db.SaveChanges() > 0)
                {
                    foreach (string image64 in dto.Images)
                    {
                        ComplaintImage CI = new ComplaintImage
                        {
                            Id = Guid.NewGuid().ToString(),
                            ComplaintId = complaint.Id,
                            Image = helper.SaveBase64(image64)
                        };
                        db.ComplaintImages.Add(CI);
                        db.SaveChanges();
                    }
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed(ResponseServices.somethingRwong);
                }
            }
        }
        
        public ResponseDTO EditComplaint(EditComplaintDTO dto)
        {
            Helper helper = new Helper();
            using (VotintSystemContext db = new VotintSystemContext())
            {
                var complaint = db.Complaints.Include(o => o.ComplaintImages).FirstOrDefault(p => p.Id == dto.Id);
                if (complaint != null)
                {
                    complaint.Title = dto.Title;
                    complaint.Description = dto.Description;
                    foreach (var item in complaint.ComplaintImages)
                    {
                        helper.deleteImage(item.Image);
                    }
                    db.ComplaintImages.RemoveRange(db.ComplaintImages.Where(p => p.ComplaintId == dto.Id).ToList());
                    db.SaveChanges();
                    foreach (string image64 in dto.Images)
                    {
                        ComplaintImage CI = new ComplaintImage
                        {
                            Id = Guid.NewGuid().ToString(),
                            ComplaintId = complaint.Id,
                            Image = helper.SaveBase64(image64)
                        };
                        db.ComplaintImages.Add(CI);
                        db.SaveChanges();
                    }
                    return responseServices.passed(mapper.Map<ComplaintDTO>(complaint));
                }
                else
                {
                    return responseServices.failed("This complaint doesn't exist in database");
                }
            }
        }

        public ResponseDTO Solved(string id)
        {
            using (VotintSystemContext db = new VotintSystemContext())
            {
                var complaint = db.Complaints.FirstOrDefault(p => p.Id == id);
                if (complaint != null)
                {
                    complaint.IsSolved = true;
                    db.SaveChanges();
                    return responseServices.passed(ResponseServices.Done);
                }
                else
                {
                    return responseServices.failed("This complaint doesn't exist in database");
                }
            }
        }

        public ResponseDTO DeleteComplaint(string id)
        {
            using (VotintSystemContext db = new VotintSystemContext())
            {
                var complaint = db.Complaints.FirstOrDefault(p => p.Id == id);
                if (complaint != null)
                {
                    complaint.IsDeleted = true;
                    return responseServices.passed(ResponseServices.Deleted);
                }
                else
                {
                    return responseServices.failed("This complaint doesn't exist in database");
                }
            }
        }
    }
}

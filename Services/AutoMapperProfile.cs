using AutoMapper;
using System;
using System.Linq;
using VotingSystemApi.DTO.Candidates;
using VotingSystemApi.DTO.Commissions;
using VotingSystemApi.DTO.Complaints;
using VotingSystemApi.DTO.Elections;
using VotingSystemApi.DTO.Post;
using VotingSystemApi.DTO.User;
using VotingSystemApi.Helpers;
using VotingSystemApi.Models;

namespace VotingSystemApi.Services
{
    public class AutoMapperProfile : Profile
    {
        private readonly VotingSystemContext db = new VotingSystemContext();
        public AutoMapperProfile()
        {
            // user maps
            CreateMap<User, UserDTO>()
                .ForMember(des => des.Image, opt => opt.MapFrom(src => Helper.serverPath + src.Image));

            CreateMap<AddUserDTO, User>();


            // post maps
            CreateMap<AddPostDTO, Post>()
                .ForMember(des => des.Date, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(des => des.IsDeleted, opt => opt.MapFrom(src => false));

            CreateMap<CommentDTO, PostComment>();
            
            CreateMap<Post, PostDTO>()
                .ForMember(des => des.LikesCount ,opt => opt.MapFrom(src => db.PostLikes.Where(p => p.PostId == src.Id).Count()))
                .ForMember(des => des.CommentsCount ,opt => opt.MapFrom(src => db.PostComments.Where(p => p.PostId == src.Id).Count()))
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(des => des.UserImage, opt => opt.MapFrom(src => src.User.Image != null ? Helper.serverPath + src.User.Image : null))
                .ForMember(des => des.images ,opt => opt.MapFrom(src => src.PostImages.Select(p => Helper.serverPath + p.Image).ToList()));



            // Elections
            CreateMap<AddElectionDTO, Election>();
            CreateMap<Election, ElectionDTO>();
            CreateMap<Election, ElectionWithDetailsDTO>()
                .ForMember(des => des.Details, opt => opt.MapFrom(src => src.Candidates.GroupBy(p => p.CommissionId).Select(x => new ElectionCommssionsDTO() 
                {
                    CommissionId = x.FirstOrDefault().CommissionId,
                    CommissionName = x.FirstOrDefault().Commission.Name,
                    candidates = x.Select(u => new CandidateDetailsDTO() 
                    {
                        UserId = u.UserId,
                        UserName = u.User.Name,
                        UserImage = u.User.Image != null ? Helper.serverPath + u.User.Image : null,
                        votes = db.Votes.Where(p => p.UserId == u.UserId && p.ElectionId == src.Id && p.CommissionId == x.FirstOrDefault().CommissionId).Count()
                    }).ToList()
                })));


            // Commissions
            CreateMap<Commission, CommissionDTO>()
                .ForMember(des => des.Image ,opt => opt.MapFrom(src => src.Image != null ? Helper.serverPath + src.Image : null));
            CreateMap<AddCommissionDTO, Commission>();


            // Complaints
            CreateMap<AddComplaintDTO, Complaint>();
            CreateMap<Complaint, ComplaintDTO>()
                .ForMember(des => des.Images, opt => opt.MapFrom(src => src.ComplaintImages.Select(x => Helper.serverPath + x.Image).ToList()))
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(des => des.UserImage, opt => opt.MapFrom(src => src.User.Image != null ? Helper.serverPath + src.User.Image : null));


            // Candidate
            CreateMap<AddCandidateDTO, Candidate>()
                .ForMember(des => des.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(des => des.IsAccepted, opt => opt.MapFrom(src => false))
                .ForMember(des => des.IsRefused, opt => opt.MapFrom(src => false))
                .ForMember(des => des.IsPending, opt => opt.MapFrom(src => true));

            CreateMap<Candidate, CandidateDTO>()
                .ForMember(des => des.CommissionName, opt => opt.MapFrom(src => src.Commission.Name))
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(des => des.UserImage, opt => opt.MapFrom(src => src.User.Image != null ? Helper.serverPath + src.User.Image : null))
                .ForMember(des => des.UserYear, opt => opt.MapFrom(src => src.User.Year))
                .ForMember(des => des.UserAcademicNo, opt => opt.MapFrom(src => src.User.AcademicNumber));
        }
    }
}

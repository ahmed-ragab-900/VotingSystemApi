using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Post;
using VotingSystemApi.Helpers;
using VotingSystemApi.Models;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Services.Posts
{
    public class PostServices : IPostServices
    {
        private readonly IResponseServices responseServices;
        private readonly IMapper mapper;
        public PostServices(IResponseServices responseServices, IMapper mapper)
        {
            this.responseServices = responseServices;
            this.mapper = mapper;
        }

        public ResponseDTO AllPosts(string userId, Filter f)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                var posts = db.Posts.Where(p => p.IsDeleted != true);
                if(f.SearchText != null && f.SearchText != "")
                {
                    posts = posts.Where(p => p.Text.Contains(f.SearchText) || p.User.Name.Contains(f.SearchText));
                }

                var currentPosts = posts.OrderByDescending(p => p.Date).Skip((f.PageNo - 1) * f.ItemsPerPage).Take(f.ItemsPerPage).Include(o => o.User).Include(o => o.PostImages);
                int postsCount = posts.Count();
                var postsDTO = mapper.Map<List<PostDTO>>(currentPosts.ToList());
                foreach (PostDTO post in postsDTO)
                {
                    post.IsLiked = db.PostLikes.Any(p => p.PostId == post.Id && p.UserId == userId);
                }

                PageOfData<PostDTO> output = new PageOfData<PostDTO>
                {
                    AllPages = Convert.ToInt32(Math.Ceiling((decimal)postsCount / f.ItemsPerPage)),
                    PageIndex = f.PageNo,
                    CurrentPageSize = postsCount - f.ItemsPerPage * f.PageNo >= f.ItemsPerPage ? f.ItemsPerPage : postsCount % f.ItemsPerPage,
                    AllItems = postsCount,
                    PageSize = f.ItemsPerPage,
                    Result = postsDTO
                };
                return responseServices.passed(output);
            }
        }

        public ResponseDTO AddPost(AddPostDTO dto)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Helper helper = new Helper();
                dto.Id = Guid.NewGuid().ToString();
                Post post = mapper.Map<Post>(dto);
                db.Posts.Add(post);
                if(db.SaveChanges() > 0)
                {
                    foreach (string image in dto.images)
                    {
                        PostImage PI = new PostImage()
                        {
                            Id = Guid.NewGuid().ToString(),
                            PostId = post.Id,
                            Image = helper.SaveBase64(image)
                        };
                        db.PostImages.Add(PI);
                        db.SaveChanges();
                    }
                    var postDto = mapper.Map<PostDTO>(post);
                    postDto.IsLiked = false;
                    return responseServices.passed(postDto);
                }
                else
                {
                    return responseServices.failed(ResponseServices.somethingRwong);
                }
            }
        }

        public ResponseDTO EditPost(string postId, EditPostDTO dto)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                Helper helper = new Helper();
                Post post = db.Posts.FirstOrDefault(p => p.Id == postId);
                if (post != null)
                {
                    post.Text = dto.Text;
                    db.PostImages.RemoveRange(db.PostImages.Where(p => p.PostId == post.Id));
                    db.SaveChanges();
                    foreach (string image in dto.images)
                    {
                        PostImage PI = new PostImage()
                        {
                            Id = Guid.NewGuid().ToString(),
                            PostId = post.Id,
                            Image = helper.SaveBase64(image)
                        };
                        db.PostImages.Add(PI);
                        db.SaveChanges();
                    }
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed("This post doesn't exist in database");
                }
            }
        }

        public ResponseDTO Like(string postId,string userId)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                PostLike PL = new PostLike()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    PostId = postId
                };
                db.PostLikes.Add(PL);
                db.SaveChanges();
                return responseServices.passedWithMessage(ResponseServices.Saved);
            }
        }
        
        public ResponseDTO DisLike(string postId,string userId)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                PostLike PL = db.PostLikes.FirstOrDefault(p => p.PostId == postId && p.UserId == userId);
                if(PL != null)
                {
                    db.PostLikes.Remove(PL);
                    db.SaveChanges();
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed(ResponseServices.somethingRwong);
                }
            }
        }

        public ResponseDTO Comment(CommentDTO dto)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                dto.Id = Guid.NewGuid().ToString();
                PostComment PC = mapper.Map<PostComment>(dto);
                db.PostComments.Add(PC);
                db.SaveChanges();
                return responseServices.passedWithMessage(ResponseServices.Saved);
            }
        }

        public ResponseDTO DeleteComment(string postId, string userId)
        {
            using (VotingSystemContext db = new VotingSystemContext())
            {
                PostComment PC = db.PostComments.FirstOrDefault(p => p.PostId == postId && p.UserId == userId);
                if (PC != null)
                {
                    db.PostComments.Remove(PC);
                    db.SaveChanges();
                    return responseServices.passedWithMessage(ResponseServices.Saved);
                }
                else
                {
                    return responseServices.failed(ResponseServices.somethingRwong);
                }
            }
        }
    }
}

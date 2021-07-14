using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Post;
using VotingSystemApi.Services.Posts;
using VotingSystemApi.Services.Response;

namespace VotingSystemApi.Controllers
{
    [ApiController , Authorize]
    public class PostsController : BaseController
    {
        private readonly IPostServices postServices;
        
        public PostsController(IPostServices postServices, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.postServices = postServices;
        }

        [Route("AllPosts"), HttpGet]
        public object AllPosts([FromQuery] Filter f)
        {
            try
            {
                var res = postServices.AllPosts(LoggedInUserId, f);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("AddPost"), HttpPost]
        public object AddPost([FromBody]AddPostDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                var res = postServices.AddPost(dto);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("EditPost/{id}"), HttpPut]
        public object EditPost(string id, [FromBody]EditPostDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                var res = postServices.EditPost(id, dto);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("Like/{id}"), HttpPut]
        public object Like(string id)
        {
            try
            {
                var res = postServices.Like(id, LoggedInUserId);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("DisLike/{id}"), HttpPut]
        public object DisLike(string id)
        {
            try
            {
                var res = postServices.DisLike(id, LoggedInUserId);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("Comment"), HttpPost]
        public object Comment([FromBody] CommentDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseServices.incorrectModel);

                var res = postServices.Comment(dto);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }

        [Route("DeleteComment"), HttpDelete]
        public object DeleteComment([FromQuery] string postId)
        {
            try
            {
                var res = postServices.DeleteComment(postId, LoggedInUserId);
                return Ok(res);
            }
            catch
            {
                return BadRequest(ResponseServices.somethingRwong);
            }
        }
    }
}

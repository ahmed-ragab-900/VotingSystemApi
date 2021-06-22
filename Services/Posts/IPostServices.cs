using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Post;

namespace VotingSystemApi.Services.Posts
{
    public interface IPostServices
    {
        public ResponseDTO AllPosts(string userId, Filter f);
        public ResponseDTO AddPost(AddPostDTO dto);
        public ResponseDTO EditPost(string postId, EditPostDTO dto);
        public ResponseDTO Like(string postId, string userId);
        public ResponseDTO DisLike(string postId, string userId);
        public ResponseDTO Comment(CommentDTO dto);
        public ResponseDTO DeleteComment(string postId, string userId);
    }
}

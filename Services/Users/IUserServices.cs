using VotingSystemApi.DTO;
using VotingSystemApi.DTO.User;

namespace VotingSystemApi.Services.Users
{
    public interface IUserServices
    {
        public ResponseDTO AllAuthorizedUsers(Filter f);
        public ResponseDTO AllUnAthorizedUsers(Filter f);
        public ResponseDTO AllWaitinUsers(Filter f);
        public ResponseDTO UserById(string id);
        public ResponseDTO Authorize(string id);
        public ResponseDTO UnAuthorize(string id);
        public ResponseDTO UpdateProfileImage(string id, string base64);
        public ResponseDTO UpdateUserDate(string id, UpdateUserDTO dto);
        public ResponseDTO UpdatePassword(string id, string newPassword);
    }
}

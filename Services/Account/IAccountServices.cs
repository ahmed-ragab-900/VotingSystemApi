using VotingSystemApi.DTO;
using VotingSystemApi.DTO.User;

namespace VotingSystemApi.Services.Account
{
    public interface IAccountServices
    {
        public ResponseDTO SignIn(string serverPath, SignInDTO dto);
        public ResponseDTO SignUp(AddUserDTO dto);
        public ResponseDTO RefreshToken(string userId);
    }
}

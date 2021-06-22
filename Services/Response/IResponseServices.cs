using VotingSystemApi.DTO;

namespace VotingSystemApi.Services.Response
{
    public interface IResponseServices
    {
        public ResponseDTO passed(dynamic data);
        public ResponseDTO passedWithMessage(string msg);
        public ResponseDTO failed(string msg);
    }
}

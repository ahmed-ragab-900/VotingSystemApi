using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Complaints;

namespace VotingSystemApi.Services.Complaints
{
    public interface IComplaintServices
    {
        public ResponseDTO AllSolvedComplaints(Filter f);
        public ResponseDTO AllNotSolvedComplaints(Filter f);
        public ResponseDTO ComplaintById(string id);
        public ResponseDTO AddComplaint(AddComplaintDTO dto);
        public ResponseDTO EditComplaint(EditComplaintDTO dto);
        public ResponseDTO Solved(string id);
        public ResponseDTO DeleteComplaint(string id);
    }
}

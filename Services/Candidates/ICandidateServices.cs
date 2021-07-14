using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Candidates;

namespace VotingSystemApi.Services.Candidates
{
    public interface ICandidateServices
    {
        public ResponseDTO AllCandidates(string electionId, Filter f);
        public ResponseDTO WaitingCandidate(string electionId);
        public ResponseDTO Commissions();
        public ResponseDTO AddCandidate(AddCandidateDTO dto);
        public ResponseDTO EditCandidate(EditCandidateDTO dto);
        public ResponseDTO DeleteCandidate(string id);
        public ResponseDTO AcceptCandidate(string id);
        public ResponseDTO RefuseCandidate(string id);
        public ResponseDTO UserIsCandidate(string userId);
    }
}

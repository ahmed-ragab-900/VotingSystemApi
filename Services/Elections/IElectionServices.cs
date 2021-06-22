using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Elections;

namespace VotingSystemApi.Services.Elections
{
    public interface IElectionServices
    {
        public ResponseDTO CurrentElection();
        public ResponseDTO ElectionById(string id);
        public ResponseDTO AllElections(Filter f);
        public ResponseDTO AllElectionsWithDetails(Filter f);
        public ResponseDTO StartElection(AddElectionDTO dto);
        public ResponseDTO CancelElection(string id);
    }
}

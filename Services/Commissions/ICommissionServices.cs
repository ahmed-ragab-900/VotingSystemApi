using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;
using VotingSystemApi.DTO.Commissions;

namespace VotingSystemApi.Services.Commissions
{
    public interface ICommissionServices
    {
        public ResponseDTO AllCommission(Filter f);
        public ResponseDTO CommissionById(string id);
        public ResponseDTO AddCommission(AddCommissionDTO dto);
        public ResponseDTO EditCommission(EditCommissionDTO dto);
        public ResponseDTO DeleteCommission(string id);
    }
}

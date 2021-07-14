using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;

namespace VotingSystemApi.Services.StudentUnion
{
    public interface IStudentUnionServices
    {
        public ResponseDTO DeleteItem(string id);
        public ResponseDTO GetItemById(string id );

    }
}

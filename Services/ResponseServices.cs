using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;

namespace VotingSystemApi.Services
{
    public class ResponseServices
    {
        public ResponseDTO passed(dynamic data)
        {
            return new ResponseDTO
            {
                IsPassed = true,
                Message = null,
                Data = data
            };
        }

        public ResponseDTO failed(string msg)
        {
            return new ResponseDTO
            {
                IsPassed = false,
                Message = msg,
                Data = null
            };
        }
    }
}

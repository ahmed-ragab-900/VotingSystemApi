using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemApi.DTO;

namespace VotingSystemApi.Services.Response
{
    public class ResponseServices : IResponseServices
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

        public ResponseDTO passedWithMessage(string msg)
        {
            return new ResponseDTO
            {
                IsPassed = true,
                Message = msg,
                Data = null
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

        public static string Saved = "Data saved successfully";
        public static string Done = "Done";
        public static string Deleted = "Data deleted successfully";
        public static string incorrectModel = "Incorrect model data";
        public static string somethingRwong = "Something went error ,please try again later";
    }
}

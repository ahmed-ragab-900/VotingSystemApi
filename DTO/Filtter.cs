using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO
{
    public class Filter
    {
        public int PageNo { get; set; }
        public int ItemsPerPage { get; set; }
        public string SearchText { get; set; }
    }
}

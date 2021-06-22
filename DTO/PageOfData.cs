using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO
{
    public class PageOfData<T>
    {
        public int AllItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPageSize { get; set; }
        public int AllPages { get; set; }
        public int PageIndex { get; set; }
        public List<T> Result { get; set; }
    }
}

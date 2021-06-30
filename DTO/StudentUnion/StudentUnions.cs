﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.DTO.StudentUnion
{
    public class StudentUnions
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CommissionId { get; set; }
        public bool? IsPresident { get; set; }
        public bool? IsAssistant { get; set; }
        public string ElectionId { get; set; }
    }
}

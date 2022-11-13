﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos
{
    public class ReportTransactionDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public float TotalAmount { get; set; }
        public float TotalRevenue { get; set; }
    }
}

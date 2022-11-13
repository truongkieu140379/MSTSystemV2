﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos.ExtendedDtos
{
    public class ExtendedTutorDto : TutorDto
    {
        public string MembershipName { get; set; }
        public string ConfirmerName { get; set; }
        public string[] CertificationUrls { get; set; } = {};
        public double AverageRatingStar { get; set; }
        public int NumberOfCourse { get; set; }
        public int NumberOfTutee { get; set; }
        public int NumberOfFeedback { get; set; }

    }
}

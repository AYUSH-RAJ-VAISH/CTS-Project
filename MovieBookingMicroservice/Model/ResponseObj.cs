﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingMicroservice.Model
{
    public class ResponseObj
    {
        public int status { get; set; }
        public string msg { get; set; }
        public Object payload { get; set; }
    }
}
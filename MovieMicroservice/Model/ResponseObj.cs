using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMicroservice.Model
{
    public class ResponseObj
    {
        public int Status { get; set; }
        public string Msg { get; set; }
        public Object Payload { get; set; }
    }
}

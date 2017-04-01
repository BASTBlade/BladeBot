using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BladeBot
{
    public class Status
    {
        public bool status { get; set; }
        public string hostname { get; set; }
        public int port { get; set; }
        public int ping { get; set; }
        public test players { get; set; }

        public struct test
        {
            public int online { get; set; }
            public int max { get; set; }
        }
    }
}

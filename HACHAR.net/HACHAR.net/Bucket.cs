using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    public abstract class Bucket
    {
        public object Key { get; set; }

        public int Value { get; set; }

        public Bucket Next { get; set; }
    }
}

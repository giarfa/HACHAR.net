using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    public class IntegerBucket : Bucket
    {
        public IntegerBucket(int key)
        {
            this.Key = key;
            this.Value = 1;
        }

        public new int Key { get; set; }

        public new IntegerBucket Next { get; set; }
    }
}

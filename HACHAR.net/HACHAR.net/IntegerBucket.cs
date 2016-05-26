using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    public class IntegerBucket : Bucket
    {
        private int key;

        public IntegerBucket(int key)
        {
            this.Key = key;
            this.Value = 1;
        }

        public override object Key { get { return this.key; } set { this.key = (int)value; } }
    }
}

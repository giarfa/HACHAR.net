using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    public class StringBucket : Bucket
    {
        public StringBucket(string key)
        {
            this.Key = key;
            this.Value = 1;
        }

        public new string Key { get; set; }

        public new StringBucket Next { get; set; }
    }
}

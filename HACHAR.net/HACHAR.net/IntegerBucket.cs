using System;

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

using System;

namespace HACHAR.net
{
    public class StringBucket : Bucket
    {
        private string key;

        public StringBucket(string key)
        {
            this.Key = key;
            this.Value = 1;
        }

        public override object Key { get { return this.key; } set { this.key = (string)value; } }
    }
}

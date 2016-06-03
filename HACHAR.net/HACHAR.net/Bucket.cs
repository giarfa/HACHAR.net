using System;

namespace HACHAR.net
{
    public abstract class Bucket
    {
        public virtual object Key { get; set; }

        public int Value { get; set; }

        public Bucket Next { get; set; }
    }
}

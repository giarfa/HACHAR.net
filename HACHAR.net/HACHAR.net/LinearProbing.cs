using System;

namespace HACHAR.net
{
    public class LinearProbing
    {
        private readonly int size;
        private readonly object locker;
        private Bucket[] buckets;

        public LinearProbing(int size, object locker)
        {
            this.size = size;
            this.locker = locker;
            this.buckets = new Bucket[this.size];
        }

        private int getBucketIndex(int key)
        {
            return key % this.size;
        }

        public void Insert(string key)
        {
            int index = this.getBucketIndex(key.GetAsciiCode());
            StringBucket b = new StringBucket(key);
            this.Insert(b, index);
        }

        public void Insert(int key)
        {
            int index = this.getBucketIndex(key);
            IntegerBucket b = new IntegerBucket(key);
            this.Insert(b, index);
        }

        private void Insert(Bucket b, int initialIndex)
        {
            int index = initialIndex;

            for (int i = 0; i < this.size; i++)
            {
                index = this.getBucketIndex(initialIndex + i);
                if (this.buckets[index] == null)
                {
                    lock (this.locker)
                    {
                        if (this.buckets[index] == null)
                        {
                            this.buckets[index] = b;
                            return;
                        }
                    }
                }
                if (b.Key.Equals(this.buckets[index].Key))
                {
                    lock (this.locker)
                    {
                        // Once  we found the key we increment the value of the counter
                        this.buckets[index].Value++;
                        return;
                    }
                }
            }
        }
    }
}

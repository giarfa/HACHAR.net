using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    public class HACHAR
    {
        private readonly int regionSize;
        private readonly Region head;
        private Region tail;
        private Region ghost;
        private readonly object locker;

        public HACHAR(int regionSize, object locker)
        {
            this.regionSize = regionSize;
            this.locker = locker;
            this.head = new Region(this.regionSize, this.locker);
            this.tail = new Region(this.regionSize, this.locker);
        }

        private int getBucketIndex(int key)
        {
            return key % this.regionSize;
        }

        public void Insert(string key)
        {
            int index = this.getBucketIndex(key.GetAsciiHACHARCode());
            StringBucket b = new StringBucket(key);
            this.Insert(b, index);
        }

        public void Insert(int key)
        {
            int index = this.getBucketIndex(key);
            IntegerBucket b = new IntegerBucket(key);
            this.Insert(b, index);
        }

        private void Insert(Bucket b, int index)
        {
            // First we try to insert in the head 
            if (this.head.Insert(b, index))
                return;

            // The slot in the head is already full so we must chain
            Bucket current = this.head.GetBucket(index);
            while (current.Next != null)
            {
                // Look for the key in the chain
                if (b.Key == current.Key)
                {
                    lock (this.locker)
                    {
                        // Once  we found the key we increment the value of the counter
                        current.Value++;
                    }
                    return;
                }
                current = current.Next;
            }
            
        }
    }
}

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
        private readonly object locker;


        public HACHAR(int regionSize, object locker)
        {
            this.regionSize = regionSize;
            this.locker = locker;
            this.head = new Region(this.regionSize, this.locker);
            this.tail = new Region(this.regionSize, this.locker);
            this.head.Next = this.tail;
        }

        public int NumberRegion
        {
            get
            {
                Region current = this.head;
                int i = 1;
                while (current.Next != null)
                {
                    i++;
                    current = current.Next;
                }
                return i;
            }
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
            Bucket last = current;
            // Look for the key in the chain
            //if (b.Key.Equals(current.Key))
            //{
            //    lock (this.locker)
            //    {
            //        // Once  we found the key we increment the value of the counter
            //        current.Value++;
            //    }
            //    return;
            //}
            while (true)
            {
                while (current != null)
                {
                    // Look for the key in the chain
                    if (b.Key.Equals(current.Key))
                    {
                        lock (this.locker)
                        {
                            // Once  we found the key we increment the value of the counter
                            current.Value++;
                            return;
                        }
                    }
                    last = current;
                    current = current.Next;
                }
                lock (this.locker)
                {
                    if (current == null)
                    {
                        if (this.tail.Insert(b))
                        {
                            //Insertion is true as we have enough space in the tail region
                            last.Next = b;
                            return;
                        }
                        //In case the insertion is false because no space in the tail region
                        Region newTail = new Region(this.regionSize, this.locker);
                        this.tail.Next = newTail;
                        this.tail = newTail;
                        this.tail.Insert(b);
                        last.Next = b;
                        return;
                    }
                }
                current = current.Next;
            }
        }
    }
}

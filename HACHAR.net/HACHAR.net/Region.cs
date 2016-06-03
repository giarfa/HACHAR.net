using System;

namespace HACHAR.net
{
    public class Region
    {
        private int nextFreeSlot;
        private int regionSize;
        private Bucket[] regionBuckets;
        private readonly object locker;

        public Region(int regionSize, object locker)
        {
            this.nextFreeSlot = 0;
            this.regionSize = regionSize;
            this.regionBuckets = new Bucket[this.regionSize];
            this.locker = locker;
        }

        public Region Next { get; set; }

        public bool Insert(Bucket bucket)
        {
            // If the region is full we must block the insertion in this region
            if (this.nextFreeSlot == this.regionSize)
                return false;

            this.regionBuckets[this.nextFreeSlot] = bucket;
            this.nextFreeSlot++;
            return true;
        }

        public bool Insert(Bucket b, int index)
        {
            // Check if the slot in the array is empty
            if (regionBuckets[index] == null)
            {
                // Lock out other threads
                lock (this.locker)
                {
                    // Check again if the slot is empty
                    if (regionBuckets[index] == null)
                    {
                        // Put the bucket in the slot
                        this.regionBuckets[index] = b;
                        return true;
                    }
                }
            }
            return false;
        }

        public Bucket GetBucket(int index)
        {
            return this.regionBuckets[index];
        }
    }
}

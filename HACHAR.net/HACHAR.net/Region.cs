using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    public class Region<T>
    {
        private int nextFreeSlot;
        private int regionSize;
        private Bucket<T>[] regionBuckets;

        public Region(int regionSize)
        {
            this.nextFreeSlot = 0;
            this.regionSize = regionSize;
            this.regionBuckets = new Bucket<T>[this.regionSize];
        }

        public bool Insert(Bucket<T> bucket)
        {
            if (this.nextFreeSlot == (this.regionSize - 1))
                return false;

            this.regionBuckets[this.nextFreeSlot] = bucket;
            this.nextFreeSlot++;
            return true;
        }

        public bool isHalfFull { get { return this.nextFreeSlot == this.regionSize / 2; } }
    }
}

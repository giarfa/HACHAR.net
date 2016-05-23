using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    public class HACHAR<T>
    {
        private readonly int regionSize;
        private readonly Region<T> head;
        private Region<T> tail;
        private Region<T> ghost;

        public HACHAR(int regionSize, T[] data)
        {
            this.regionSize = regionSize;
            this.head = new Region<T>(regionSize);
            this.tail = new Region<T>(regionSize);
            this.ghost = new Region<T>(regionSize);

            
        }

        public void Start()
        { }
    }
}

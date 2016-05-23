using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    public class Bucket<T>
    {
        public T Key { get; set; }

        public int Value { get; set; }

        public Bucket<T> Next { get; set; }
    }
}

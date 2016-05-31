using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    public static class Extensions
    {
        public static int GetAsciiCode(this String s)
        {
            int hash = 0;
            for (int i = 0; i < s.Length; i++)
            {
                hash += s[i];
            }
            return hash;
        }
    }
}

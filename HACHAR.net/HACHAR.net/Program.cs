using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("add".GetHashCode());

            Console.WriteLine("supercalifragilisticexpialidocious".ToAsciiHACHARCode());

            Console.WriteLine(myHash("add"));



            Console.ReadLine();
        }

        static int myHash(String toHash)
        {
            int hash = 0;
            char a;
            for (int i = 0; i < toHash.Length; i++)
            {
                hash += toHash[i];
            }
            return hash;
        }
    }
}

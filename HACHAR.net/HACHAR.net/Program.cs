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
            //string nerdData = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
            string nerdData = "bla one two a d j a g  d a j a";
            Object locker = new Object();
            HACHAR h = new HACHAR(3, locker);
            //Console.WriteLine("add".GetHashCode());

            //Console.WriteLine("supercalifragilisticexpialidocious".GetAsciiHACHARCode());
            

            foreach (string item in nerdData.Split(' '))
            {
                h.Insert(item);

            }
            Console.WriteLine(h.NumberRegion);
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACHAR.net
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] AllLines = new string[int.MaxValue/5000]; //only allocate memory here
            string filePath = @"C:\Users\Francesco\Downloads\1.txt";
            //string filePath = @"C:\Users\Francesco\Downloads\Charlie_and_the_Chocolate_Factory-Dahl_Roald.txt";
            AllLines = File.ReadAllLines(filePath);

            int[] numbers = new int[] { 4, 10, 8, 6, 9, 2, 6, 12, 16, 21, 34, 9, 6, 2, 6 };

            Object locker = new Object();

            HACHAR h;
            long sum=0;
            long partial = 0;

            Console.WriteLine("No Options");            
            for (int i = 0; i < 5; i++)
            {
                h = new HACHAR(2029, locker);
                partial = doIt(AllLines, h);
                sum += partial;
                Console.Write(partial + "; ");
            }
            Console.WriteLine("avg: " + (sum / 5));


            for (int i = 1; i <= 8; i++)
            {
                sum = 0;
                Console.WriteLine(i + " cores");
                for (int j = 0; j < 5; j++)
                {
                    h = new HACHAR(2029, locker);
                    partial = doIt(AllLines, h, i);
                    sum += partial;
                    Console.Write(partial + "; ");
                }
                Console.WriteLine("avg: " + (sum / 5));
            }

            Console.WriteLine("DONE");
            Console.ReadLine();
        }

        static long doIt(string[] lines, HACHAR h)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();


            Parallel.ForEach(lines, item =>
            {
                foreach (var s in item.Split(' '))
                {
                    h.Insert(item);
                }
            });

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        static long doIt(string[] lines, HACHAR h, int cores)
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = cores
            };

            Stopwatch watch = new Stopwatch();
            watch.Start();


            Parallel.ForEach(lines, options, item =>
             {
                 foreach (var s in item.Split(' '))
                 {
                     h.Insert(item);
                 }
             });

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        static long doIt(string[] lines, LinearProbing l)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();


            Parallel.ForEach(lines, item =>
            {
                foreach (var s in item.Split(' '))
                {
                    l.Insert(item);
                }
            });

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        static long doIt(string[] lines, LinearProbing l, int cores)
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = cores
            };

            Stopwatch watch = new Stopwatch();
            watch.Start();


            Parallel.ForEach(lines, options, item =>
            {
                foreach (var s in item.Split(' '))
                {
                    l.Insert(item);
                }
            });

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }
    }
}

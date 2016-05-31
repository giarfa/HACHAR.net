using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HACHAR.net
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 4, 10, 8, 6, 9, 2, 6, 12, 16, 21, 34, 9, 6, 2, 6 };
            Object locker = new Object();
            string filePath = @"C:\Users\Mehdi Kaabi\Desktop\1.txt";
            Console.WriteLine("Test_HACHAR_Strings_Partion");
            Test_HACHAR_Strings_Partion(locker, filePath);

            Console.WriteLine("Test_HACHAR_Strings");
            Test_HACHAR_Strings(locker, filePath);

            Console.WriteLine("Test_LinearProbing_Strings");
            Test_LinearProbing_Strings(locker, filePath);

            Console.WriteLine("DONE");
            Console.ReadLine();
        }

        private static void Test_HACHAR_Strings(object locker, string filePath)
        {
           
            //string filePath = @"C:\Users\Francesco\Downloads\Charlie_and_the_Chocolate_Factory-Dahl_Roald.txt";
            string[] AllLines = File.ReadAllLines(filePath);

            HACHAR h;
            long sum = 0;
            long partial = 0;
            int size = 2029;

            Console.WriteLine("No Options");
            for (int i = 0; i < 5; i++)
            {
                h = new HACHAR(size, locker);
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
                    h = new HACHAR(size, locker);
                    partial = doIt(AllLines, h, i);
                    sum += partial;
                    Console.Write(partial + "; ");
                }
                Console.WriteLine("avg: " + (sum / 5));
            }
        }

        private static void Test_HACHAR_Strings_Partion(object locker, string filePath)
        {
            
            //string filePath = @"C:\Users\Francesco\Downloads\Charlie_and_the_Chocolate_Factory-Dahl_Roald.txt";
            string[] AllLines = File.ReadAllLines(filePath);

            HACHAR h = new HACHAR(2029, locker);
            OrderablePartitioner<Tuple<int, int>> chunkPart =
             Partitioner.Create(0, AllLines.Length, 2500);

            Stopwatch watch = new Stopwatch();

            watch.Start();
            Parallel.ForEach(chunkPart, chunkRange =>
            {

                for (int i = chunkRange.Item1; i < chunkRange.Item2; i++)
                {
                    foreach (string s in AllLines[i].Split(' '))
                    {
                        h.Insert(s);
                    }

                }
            });

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        private static void Test_LinearProbing_Strings(object locker, string filePath)
        {
            
            //string filePath = @"C:\Users\Francesco\Downloads\Charlie_and_the_Chocolate_Factory-Dahl_Roald.txt";
            string[] AllLines = File.ReadAllLines(filePath);

            LinearProbing l;
            long sum = 0;
            long partial = 0;
            int size = 50551;

            Console.WriteLine("No Options");
            for (int i = 0; i < 5; i++)
            {
                l = new LinearProbing(size, locker);
                partial = doIt(AllLines, l);
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
                    l = new LinearProbing(size, locker);
                    partial = doIt(AllLines, l, i);
                    sum += partial;
                    Console.Write(partial + "; ");
                }
                Console.WriteLine("avg: " + (sum / 5));
            }
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

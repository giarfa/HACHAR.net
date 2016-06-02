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
            Object locker = new Object();

            Console.WriteLine("START\n");

            #region Tests with Strings

            string filePath = @"C:\Users\Francesco\Downloads\1.txt";
            //string filePath = @"C:\Users\Francesco\Downloads\Charlie_and_the_Chocolate_Factory-Dahl_Roald.txt";
            string[] AllLines = File.ReadAllLines(filePath);

            //Console.WriteLine("Test_HACHAR_Strings_Partion");
            //Test_HACHAR_Strings_Partion(locker, AllLines);

            Console.WriteLine("Test_HACHAR_Strings");
            Test_HACHAR_Strings(locker, AllLines);

            Console.WriteLine("Test_LinearProbing_Strings");
            Test_LinearProbing_Strings(locker, AllLines);

            #endregion

            #region Tests with integers

            int max = 1000000;
            int[] numbers = new int[max];
            Random rnd = new Random();
            for (int i = 0; i < max; i++)
            {
                numbers[i] = rnd.Next(max);
            }

            //Console.WriteLine("Test_HACHAR_Integers_Partion");
            //Test_HACHAR_Integers_Partion(locker, numbers);

            Console.WriteLine("Test_HACHAR_Integers");
            Test_HACHAR_Integers(locker, numbers);

            Console.WriteLine("Test_LinearProbing_Integers");
            Test_LinearProbing_Integers(locker, numbers);

            #endregion

            Console.WriteLine("\nDONE");
            Console.ReadLine();
        }

        static void Test_HACHAR_Strings(object locker, string[] AllLines)
        {
            HACHAR h;
            long sum = 0;
            long partial = 0;
            int size = 2029;

            // Spare call in order to exploit the JIT Compilation
            Console.WriteLine("Spare call");
            h = new HACHAR(size, locker);
            partial = doIt(AllLines, h);
            Console.WriteLine(partial);

            Console.WriteLine("No Options");
            for (int i = 0; i < 5; i++)
            {
                h = new HACHAR(size, locker);
                partial = doIt(AllLines, h);
                sum += partial;
                Console.Write(partial + "; ");
            }
            Console.WriteLine("avg: " + (sum / 5));

            // Spare call in order to exploit the JIT Compilation
            Console.WriteLine("Spare call");
            h = new HACHAR(size, locker);
            partial = doIt(AllLines, h, 1);
            Console.WriteLine(partial);

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

        static void Test_HACHAR_Strings_Partion(object locker, string[] AllLines)
        {
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

        static void Test_LinearProbing_Strings(object locker, string[] AllLines)
        {
            LinearProbing l;
            long sum = 0;
            long partial = 0;
            int size = 50551;

            // Spare call in order to exploit the JIT Compilation
            Console.WriteLine("Spare call");
            l = new LinearProbing(size, locker);
            partial = doIt(AllLines, l);
            Console.WriteLine(partial);

            Console.WriteLine("No Options");
            for (int i = 0; i < 5; i++)
            {
                l = new LinearProbing(size, locker);
                partial = doIt(AllLines, l);
                sum += partial;
                Console.Write(partial + "; ");
            }
            Console.WriteLine("avg: " + (sum / 5));

            // Spare call in order to exploit the JIT Compilation
            Console.WriteLine("Spare call");
            l = new LinearProbing(size, locker);
            partial = doIt(AllLines, l, 1);
            Console.WriteLine(partial);

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

        static void Test_HACHAR_Integers(object locker, int[] numbers)
        {
            HACHAR h;
            long sum = 0;
            long partial = 0;
            int size = 110233;

            // Spare call in order to exploit the JIT Compilation
            Console.WriteLine("Spare call");
            h = new HACHAR(size, locker);
            partial = doIt(numbers, h);
            Console.WriteLine(partial);

            Console.WriteLine("No Options");
            for (int i = 0; i < 5; i++)
            {
                h = new HACHAR(size, locker);
                partial = doIt(numbers, h);
                sum += partial;
                Console.Write(partial + "; ");
            }
            Console.WriteLine("avg: " + (sum / 5));

            // Spare call in order to exploit the JIT Compilation
            Console.WriteLine("Spare call");
            h = new HACHAR(size, locker);
            partial = doIt(numbers, h, 1);
            Console.WriteLine(partial);

            for (int i = 1; i <= 8; i++)
            {
                sum = 0;
                Console.WriteLine(i + " cores");
                for (int j = 0; j < 5; j++)
                {
                    h = new HACHAR(size, locker);
                    partial = doIt(numbers, h, i);
                    sum += partial;
                    Console.Write(partial + "; ");
                }
                Console.WriteLine("avg: " + (sum / 5));
            }
        }

        static void Test_HACHAR_Integers_Partion(object locker, int[] numbers)
        {
            HACHAR h = new HACHAR(110233, locker);
            OrderablePartitioner<Tuple<int, int>> chunkPart =
             Partitioner.Create(0, numbers.Length, 2000);

            Stopwatch watch = new Stopwatch();

            watch.Start();
            Parallel.ForEach(chunkPart, chunkRange =>
            {

                for (int i = chunkRange.Item1; i < chunkRange.Item2; i++)
                {
                    h.Insert(numbers[i]);
                }
            });

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        static void Test_LinearProbing_Integers(object locker, int[] numbers)
        {
            LinearProbing l;
            long sum = 0;
            long partial = 0;
            int size = 1028777;

            // Spare call in order to exploit the JIT Compilation
            Console.WriteLine("Spare call");
            l = new LinearProbing(size, locker);
            partial = doIt(numbers, l);
            Console.WriteLine(partial);

            Console.WriteLine("No Options");
            for (int i = 0; i < 5; i++)
            {
                l = new LinearProbing(size, locker);
                partial = doIt(numbers, l);
                sum += partial;
                Console.Write(partial + "; ");
            }
            Console.WriteLine("avg: " + (sum / 5));

            // Spare call in order to exploit the JIT Compilation
            Console.WriteLine("Spare call");
            l = new LinearProbing(size, locker);
            partial = doIt(numbers, l, 1);
            Console.WriteLine(partial);

            for (int i = 1; i <= 8; i++)
            {
                sum = 0;
                Console.WriteLine(i + " cores");
                for (int j = 0; j < 5; j++)
                {
                    l = new LinearProbing(size, locker);
                    partial = doIt(numbers, l, i);
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
                foreach (string s in item.Split(' '))
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
                 foreach (string s in item.Split(' '))
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
                foreach (string s in item.Split(' '))
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
                foreach (string s in item.Split(' '))
                {
                    l.Insert(item);
                }
            });

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        static long doIt(int[] numbers, HACHAR h)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Parallel.ForEach(numbers, item =>
            {
                h.Insert(item);
            });

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        static long doIt(int[] numbers, HACHAR h, int cores)
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = cores
            };

            Stopwatch watch = new Stopwatch();
            watch.Start();

            Parallel.ForEach(numbers, options, item =>
            {
                h.Insert(item);
            });

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        static long doIt(int[] numbers, LinearProbing l)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Parallel.ForEach(numbers, item =>
            {
                l.Insert(item);
            });

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        static long doIt(int[] numbers, LinearProbing l, int cores)
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = cores
            };

            Stopwatch watch = new Stopwatch();
            watch.Start();

            Parallel.ForEach(numbers, options, item =>
            {
                l.Insert(item);
            });

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }
    }
}

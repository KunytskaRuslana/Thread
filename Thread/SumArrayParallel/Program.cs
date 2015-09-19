using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SumArrayParallel
{
    class Program
    {
        private static int _sumArray = 0;
        private static int colRows = 0;
        private static int colColumns = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("*** Sum Array Parallel ***");
            Console.WriteLine();
            Console.Write("Enter the number of rows: ");
            colRows = Int32.Parse(Console.ReadLine());
            Console.Write("Enter the number of columns: ");
            colColumns = Int32.Parse(Console.ReadLine());

            Random rand = new Random();

            int[,] doubleArray = new int[colRows, colColumns];

            for (int i = 0; i < colRows; i++)
            {
                for (int j = 0; j < colColumns; j++)
                {
                    doubleArray[i, j] = rand.Next(1, 100);
                }
            }

            for (int i = 0; i < colRows; i++)
            {
                for (int j = 0; j < colColumns; j++)
                {
                    Console.Write(doubleArray[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SumArray(doubleArray);
            stopwatch.Stop();

            Console.ReadKey();
        }

        private static void SumArray(object obj)
        {
            int[,] n = (int[,])obj;
            int s = 0;
            int[] m = new int[colColumns];
            Parallel.For(0, m.Length, k =>
                {
                    for (int i = 0; i < colRows; i++)
                    {
                        for (int j = 0; j < colColumns; j++)
                        {
                            m[j] = n[i, j];
                        }
                        Thread.Sleep(10000);
                        int _sumRow = 0;

                        Parallel.For(0, m.Length, l =>
                        {
                            for (int o = 0; o < m.Length; o++)
                            {
                                _sumRow += m[o];
                            }
                            _sumArray += _sumRow;
                            Console.WriteLine("Sum Row: {0}", _sumRow);
                        });
                        Thread.Sleep(10000);
                        Console.WriteLine("*****");
                    }
                });
            Console.WriteLine("The sum of array elements: {0}", _sumArray);
        }
        private static void SumRow(object obj)
        {
            int[] n = (int[])obj;
            int _sumRow = 0;

            Parallel.For(0, n.Length, k =>
                {
                    for (int i = 0; i < n.Length; i++)
                    {
                        _sumRow += n[i];
                    }
                    _sumArray += _sumRow;
                    Console.WriteLine("Sum Row: {0}", _sumRow);
                });
        }
    }
}

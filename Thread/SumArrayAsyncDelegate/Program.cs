using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SumArrayAsyncDelegate
{
    class Program
    {
        private static int _sumArray = 0;
        private static int _sumRow = 0;
        private static int colRows = 0;
        private static int colColumns = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("*** Sum Array Async Delegate ***");
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

            Func<int[,], int> a = SumArray;

            IAsyncResult iAR = a.BeginInvoke(doubleArray,null,null);

            _sumArray = a.EndInvoke(iAR);
            Console.WriteLine("The sum of array elements: {0}", _sumArray);

            Console.ReadKey();
        }

        private static int SumArray(object obj)
        {
            int[,] n = (int[,])obj;
            int s = 0;
            int[] m = new int[colColumns];
            for (int i = 0; i < colRows; i++)
            {
                for (int j = 0; j < colColumns; j++)
                {
                    m[j] = n[i, j];
                }

                Func<int[], int> b = SumRow;

                IAsyncResult iAR = b.BeginInvoke(m, null, null);

                _sumArray = b.EndInvoke(iAR);

                Thread.Sleep(100);

                Console.WriteLine("Sum Row: {0}", _sumRow);
                Console.WriteLine("*****");
                _sumArray += _sumRow;
                _sumRow = 0;
            }
            return _sumArray;
        }
        private static int SumRow(object obj)
        {
            int[] n = (int[])obj;
            //int _sumRow = 0;

            for (int i = 0; i < n.Length; i++)
            {
                _sumRow += n[i];
            }
            //_sumArray += _sumRow;
            return _sumRow;
        }
    }
}

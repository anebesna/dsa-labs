using System;
using static System.Console;

namespace asd_lab3
{
    class Program
    {
        public static void Main()
        {
            int N, M, K;
            Write("Enter N: ");
            N = int.Parse(ReadLine());
            Write("Enter M: ");
            M = int.Parse(ReadLine());
            Write("Enter K: ");
            K = int.Parse(ReadLine());
            int[,] matrix = new int[N, M];
            Generate(N, M, matrix, K);
            WriteLine();
            Output(matrix, K, N, M);
        }
        static void Generate(int n, int m, int[,] arr, int k)
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (NSK(j, k) % 2 == 1) ForegroundColor = ConsoleColor.DarkBlue;
                    else ResetColor();
                    arr[i, j] = rnd.Next(0, 51);
                    Write(arr[i, j] + "\t");
                }
                WriteLine();
            }
        }
        static void Output(int[,] arr, int k, int n, int m)
        {
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if(NSK(j, k) % 2 == 1)
                    {
                        count++;
                    }
                }
            }
            int b = 0;
            int[] a = new int[count];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (NSK(j, k) % 2 == 1)
                    {
                        a[b] = arr[i, j];
                        b++;
                    }
                }
            }
            int[] a_sorted = Sort(a, count);
            b = 0;
            for (int j = 0; j < m; j++)
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    if (NSK(j, k) % 2 == 1)
                    {
                        arr[i, j] = a_sorted[b];
                        b++;
                    }
                }
            }
            PrintMatrix(arr, n, m, k);
        }
        static int[] Sort(int[] arr, int l)
        {
            bool swap = true;
            while (swap)
            {
                swap = false;
                for (int i = 0; i < l; i++)
                {
                    if (i == l-1) break;
                    if (arr[i] > arr[i + 1])
                    {
                        int a = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = a;
                        swap = true;
                    }
                }
            }
            return arr;
        }
        static int NSK(int j, int k)
        {
            int nsd = 1;
            for (int d = 1; d < j*k+1; d++)
            {
                if (j % d == 0 && k % d == 0)
                {
                    nsd = d; 
                }
            }
            return j * k / nsd;
        }
        static void PrintMatrix(int[,] b, int n, int m, int k)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (NSK(j, k) % 2 == 1) ForegroundColor = ConsoleColor.DarkBlue;
                    else ResetColor();
                    Write(b[i, j] + "\t");
                }
                WriteLine();
            }
        }
    }
}
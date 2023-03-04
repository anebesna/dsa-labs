using System;
using static System.Console;

namespace asd_lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Write("\nEnter the command or press /help to see the available commands: ");
                    string command = ReadLine();
                    if (command == "/help")
                    {
                        Write("/help - list of commands\n/ctrl - to generate control matrix\n/rnd - to generate random matrix\n/clear - to clear terminal\n/exit - exit");
                    }
                    else if (command == "/ctrl") Generate_C();

                    else if (command == "/rnd") Generate_Rnd();
                    else if (command == "/clear") Clear();
                    else if (command == "/exit") Environment.Exit(0);
                    else WriteLine("Wrong command");

                }
                catch
                {
                    WriteLine("Error");
                }
            }
        }
        static void Generate_Rnd()
        {
            Write("Enter the size of matrix: ");
            int n = int.Parse(ReadLine());
            Random rnd = new Random();
            int[,] nums = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    nums[i, j] = rnd.Next(0, 50);
                    Write("{0,5}", nums[i, j] + " | ");
                }
                WriteLine();
            }
            Vmat(nums, n);
            Diagonal(nums, n);
            Hmat(nums, n);
        }
        static void Generate_C()
        {
            Write("Enter the size of matrix: ");
            int n = int.Parse(ReadLine());
            int[,] nums = new int[n, n];
            int k = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    nums[i, j] = k;
                    k++;
                    Write("{0,5}", nums[i, j] + " | ");
                }
                WriteLine();
            }
            Vmat(nums, n);
            Diagonal(nums, n);
            Hmat(nums, n);
        }
        static void Vmat(int[,] nums, int n)
        {
            int max, min, i1, i2, j1, j2;
            max = min = nums[0, n-2];
            i1 = i2 = 0;
            j1 = j2 = n-2;
            for (int j = n-2; j >= 0; j--)
            {
                if (j % 2 == 0)
                {
                    for (int i = 0; j < n; i++)
                    {
                        if (i + j == n-1) break;
                        Write(nums[i, j] + " ");
                        (min, i1, j1) = Min(nums, i, j, min, i1, j1);
                        (max, i2, j2) = Max(nums, i, j, max, i2, j2);
                    }
                }
                else
                { 
                    for (int i = n-2-j; i >= 0; i--)
                    {
                        if (i + j == n-1) break;
                        Write(nums[i, j] + " ");
                        (min, i1, j1) = Min(nums, i, j, min, i1, j1);
                        (max, i2, j2) = Max(nums, i, j, max, i2, j2);
                    }
                }
            }
            WriteLine($"\nMax value above the diagonal: {max} ({i2},{j2})\nMin value above the diagonal: {min} ({i1},{j1})\n");
        }
        static void Diagonal(int[,] nums, int n)
        {
            int max, min, i1, i2, j1, j2;
            max = min = nums[n-1, 0];
            i1 = i2 = n-1;
            j1 = j2 = 0;
            for (int i = n-1; i >= 0; i--)
            {
                for (int j = 0; j <= nn; j++)
                {
                    if (i + j == n-1)
                    {
                        Write(nums[i, j] + " ");
                        (min, i1, j1) = Min(nums, i, j, min, i1, j1);
                        (max, i2, j2) = Max(nums, i, j, max, i2, j2);
                    }
                }
            }
            WriteLine($"\nMax value on the diagonal: {max} ({i2},{j2})\nMin value on the diagonal: {min} ({i1},{j1})\n");
        }
        static void Hmat(int[,] nums, int n) {
            int max, min, i1, j1, i2, j2;
            max = min = nums[1, n-1];
            i1 = i2 = 1;
            j1 = j2 = n-1;
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = n - i; j < n; j++)
                    {
                        if (i + j == n-1) break;
                        Write(nums[i, j] + " ");
                        (min, i1, j1) = Min(nums, i, j, min, i1, j1);
                        (max, i2, j2) = Max(nums, i, j, max, i2, j2);
                    }
                }
                else
                {
                    for (int j = n-1; j > 0; j--)
                    {
                        if (i + j == n-1) break;
                        (min, i1, j1) = Min(nums, i, j, min, i1, j1);
                        (max, i2, j2) = Max(nums, i, j, max, i2, j2);
                        Write(nums[i, j] + " ");

                    }
                }
            }
            WriteLine($"\nMax value under the diagonal: {max} ({i2},{j2})\nMin value under the diagonal: {min} ({i1},{j1})\n");
        }
        static (int,int,int) Min(int[,] nums, int i, int j, int min, int i1, int j1)
        {
            if (min > nums[i, j])
            {
                min = nums[i, j];
                i1 = i;
                j1 = j;
            }
            return (min, i1, j1);
        }
        static (int, int, int) Max(int[,] nums, int i, int j, int max, int i2, int j2)
        {
            if (max < nums[i, j])
            {
                max = nums[i, j];
                i2 = i;
                j2 = j;
            }
            return (max, i2, j2);
        }
    }
}

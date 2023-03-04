using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;

namespace asd_lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter your space-separated array of quinary numbers: ");
            string nums = ReadLine();
            int[] arr = Array.ConvertAll(nums.Split(' '), int.Parse);
            char[] check = { '5', '6', '7', '8', '9' };
            while (check.Any(s => nums.Contains(s)))
            {
                WriteLine("There is non-quinary value, try again!");
                Write("Enter your space-separated array of numbers: ");
                nums = ReadLine();
                arr = Array.ConvertAll(nums.Split(' '), int.Parse);
            }
            int N = arr.Length;
            while (true)
            {
                try
                {
                    Write("\nEnter the command or write /help to see the available commands: ");
                    string command = ReadLine();
                    if (command == "/help")
                    {
                        Write("/help\n/sort\n/newarray\n/example\n/exit");
                    }
                    else if (command == "/sort") RadixSort(arr, N);
                    else if (command == "/newarray")
                    {
                        Write("Enter the new space-separated array of numbers: ");
                        nums = ReadLine();
                        arr = Array.ConvertAll(nums.Split(' '), int.Parse);
                        N = arr.Length;
                    }
                    else if (command == "/example") Test();
                    else if (command == "/exit") Environment.Exit(0);
                    else WriteLine("Wrong command");
                }
                catch
                {
                    WriteLine("Error");
                }
            }
        }

        static void Test()
        {
            int[] arr = { 22, 14, 13, 11, 12, 21, 3, 10, 20, 444, 2 };
            RadixSort(arr, arr.Length);
        }

        static void RadixSort(int[] fullarr, int n)
        {
            (int[] arr, int[] rest) = CheckedArray(fullarr, n);
            int digit;
            int m = Max(arr);
            for (digit = 1; m / digit > 0; digit *= 10)
            {
                arr = CountSort(arr, digit);
            }
            Write("Current array: \n");
            Print(fullarr, rest);
            Write("\nRadix sorted array: \n");
            Print(arr, rest);
            Print(rest, rest);
        }
        static (int[], int[]) CheckedArray(int[] arr, int n)
        {
            List<int> temp = new List<int>();
            List<int> rest = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (ToBaseTen(arr[i]) >= n / 2 && ToBaseTen(arr[i]) <= n * n)
                {
                    temp.Add(arr[i]);
                }
                else rest.Add(arr[i]);
            }
            rest = Reversed(rest);
            return (temp.ToArray(), rest.ToArray());
        }

        static int[] CountSort(int[] arr, int digit)
        {
            int[] result = new int[arr.Length];
            int[] count = { 0, 0, 0, 0, 0 };
            for (int i = 0; i < arr.Length; i++) count[arr[i] / digit % 10]++;
            for (int i = 1; i < 5; i++) count[i] += count[i - 1];
            for (int i = arr.Length-1; i >= 0; i--)
            {
                result[count[arr[i] / digit % 10] - 1] = arr[i];
                count[arr[i] / digit % 10]--;
            }
            return result;
        }

        static int ToBaseTen(int num)
        {
            int res = 0;
            int rest;
            int i = 0;
            while (num > 0)
            {
                rest = num % 10;
                res += rest * Pow(5, i);
                i++;
                num /= 10;
            }
            return res;
        }
        
        static int Pow(int num, int pow)
        {
            if (pow == 0) return 1;
            else
            {
                for (int i = 1; i < pow; i++) num *= num;
                return num;
            }
        }

        static int Max(int[] arr)
        {
            int max = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max) max = arr[i];
            }
            return max;
        }
        static void Print(int[] arr, int[] invalid)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if(!invalid.Contains(arr[i])) ForegroundColor = ConsoleColor.DarkBlue;
                else ResetColor();
                Write(arr[i] + "\t");
            }
        }
        static List<int> Reversed(List<int> list)
        {
            int temp;
            int j = list.Count - 1;
            for(int i = 0; i<list.Count/2; i++)
            {
                temp = list[i];
                list[i] = list[j];
                list[j] = temp;
                j--;
            }
            return list;
        }
    }
}
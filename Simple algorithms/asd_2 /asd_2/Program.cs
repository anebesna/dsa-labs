using System;
using static System.Console;
using static System.Math;

namespace asd1
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter the year: ");
            int n = int.Parse(ReadLine());
            int k = 0;
            int y = n % 100;
            int c = n / 100;
            for (int d = 0; d <= 31; d++)
            {
                if (((int)(2.6 * 8 - 0.2) + d + y + y / 4 + c / 4 - 2 * c) % 7 == 0)
                {
                    k = d;
                }
            }
            Write(k);
        }
    }
}
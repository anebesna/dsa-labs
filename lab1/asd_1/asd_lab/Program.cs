using System;
using static System.Console;
using static System.Math;

namespace cs4
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, y, z, k;
            x = double.Parse(ReadLine());
            y = double.Parse(ReadLine());
            z = double.Parse(ReadLine());
            k = Pow(x, -y) + z / (y * y + 1) + Cbrt(y / (x * z + 2));
            if ((k / PI + 1 / 2) % 1 != 0 && Cbrt(x * z) != 0 && x * z >= 0 && ((x != 0 && y != 0) || y <= -1 || (x == 0 && y < 0) || (x > 0 && y != 0)) && z != 0 && Sin((x + PI * y) / z) != 0)
            {
                WriteLine($"a = {Tan(k)}\nb = {Tan(k) / Sin((x + PI * y) / z)}");
            }
            else Write("Error");
        }
    }
}

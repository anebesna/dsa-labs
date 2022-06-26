using System;
using static System.Console;
using System.IO;

namespace lab_7
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamReader sr = new StreamReader("text.txt");
                StreamWriter sw = new StreamWriter("result.txt");
                string str = sr.ReadToEnd();
                WriteLine(str);
                string s = ReadLine();
                string[] lines = str.Split('\n');
                GetIndex(s, lines, sw);
                sw.Close();
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
                return;
            }
        }
        static void GetIndex(string s, string[] l, StreamWriter sw)
        {
            int line = 0;
            sw.WriteLine("-Line- -Index-");
            foreach (string k in l)
            {
                int lastIndex = 0;
                while (lastIndex != -1) {
                    lastIndex = k.IndexOf(s, lastIndex);
                    if (lastIndex != -1)
                    {
                        sw.WriteLine($"  {line}      {lastIndex}");
                        lastIndex++;
                    }     
                }
                line++;
            }
            
        }
    }
}
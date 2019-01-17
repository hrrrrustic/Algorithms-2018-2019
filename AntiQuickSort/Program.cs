using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AntiQuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, help;
            using (var file = new StreamReader("antiqs.in"))
            {
            n = int.Parse(Console.ReadLine());
            }
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
                a[i] = i + 1;
            for (int i = 2; i < n; i++)
            {
                help = a[i];
                a[i] = a[i / 2];
                a[i / 2] = help;
            }
            using (var outfile = new StreamWriter("antiqs.out"))
            {
            Console.WriteLine(string.Join(" ", a));
            }
        }
    }
}

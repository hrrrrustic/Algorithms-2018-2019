using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Merge_Sort
{
    class MergeSort
    {
        static int[] Merge_Sort(int[] massive)
        {
            if (massive.Length == 1)
                return massive;
            int mid_point = massive.Length / 2;
            return Merge(Merge_Sort(massive.Take(mid_point).ToArray()), Merge_Sort(massive.Skip(mid_point).ToArray()));
        }

        static int[] Merge(int[] mass1, int[] mass2)
        {
            int a = 0, b = 0;
            int[] merged = new int[mass1.Length + mass2.Length];
            for (int i = 0; i < mass1.Length + mass2.Length; i++)
            {
                if (b < mass2.Length && a < mass1.Length)
                    if (mass1[a] > mass2[b])
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
                else
                    if (b < mass2.Length)
                    merged[i] = mass2[b++];
                else
                    merged[i] = mass1[a++];
            }
            return merged;
        }
        static void Solve(string[] args)
        {
            using (var file = new StreamReader("sort.in"))
            {
            string t;
            int s = int.Parse(Console.ReadLine());
            int[] arr = new int[s];
            t = Console.ReadLine();
            string[] list = t.Split(' ');
            for (int i = 0; i < arr.Length; i++)
                arr[i] = int.Parse(list[i]);
            arr = Merge_Sort(arr);
            using (var outfile = new StreamWriter("sort.out"))
            {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
        }
            }
        }
    }
}
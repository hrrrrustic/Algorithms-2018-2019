using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SortingAlgorithms
{
    class MergeSortForRunners
    {
        static List<string> Merge_Sort(List<string> massive)
        {
            if (massive.Count == 1)
                return massive;

            int mid_point = massive.Count / 2;
            return Merge(Merge_Sort(massive.Take(mid_point).ToList()), Merge_Sort(massive.Skip(mid_point).ToList()));
        }
        static List<string> Merge(List<string> mass1, List<string> mass2)
        {
            int a = 0, b = 0;
            List<string> merged = new List<string>(mass1.Count + mass2.Count);
            for (int i = 0; i < mass1.Count + mass2.Count; i++)
            {
                if (b < mass2.Count && a < mass1.Count)
                    if (String.CompareOrdinal(mass1[a], mass2[b]) > 0)
                    {
                        merged.Add(mass2[b]);
                        b++;
                    }
                    else
                    {
                        merged.Add(mass1[a]);
                        a++;
                    }
                else
                    if (b < mass2.Count)
                {
                    merged.Add(mass2[b]);
                    b++;
                }
                else
                {
                    merged.Add(mass1[a]);
                    a++;
                }
            }
            return merged;
        }
        static void Solve()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            using (var file = new StreamReader("input.txt"))
            {
                int s = int.Parse(file.ReadLine());
                for (int i = 0; i < s; i++)
                {
                    var t = file.ReadLine();
                    string[] list = t.Split(' ');

                    if (dict.ContainsKey(list[0]))
                    {
                        dict[list[0]].Add(list[1]);
                    }
                    else
                    {
                        dict[list[0]] = new List<string> { list[1] };
                    }
                }
                List<string> sortedCountry = Merge_Sort(dict.Keys.ToList());
                using (var outfile = new StreamWriter("output.txt"))
                {
                    foreach (string j in sortedCountry)
                    {
                        List<string> sp = dict[j];
                        outfile.WriteLine("=== " + j + " ===");
                        for (int i = 0; i < sp.Count; i++)
                        {
                            outfile.WriteLine(sp[i]);
                        }
                    }
                }
            }
        }
    }
}
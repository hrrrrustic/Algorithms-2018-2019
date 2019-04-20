using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Radix_Sort
{
    class RadixSort
    {
        static void Solve()
        {
            int[] ArrayOfParams;
            string[] InputStrings;
            using (var file = new StreamReader("radixsort.in"))
            {
                ArrayOfParams = Console.ReadLine().Split(' ').Select(k => int.Parse(k)).ToArray();
                InputStrings = new string[ArrayOfParams[0]];
                for (int i = 0; i < ArrayOfParams[0]; i++)
                {
                    InputStrings[i] = Console.ReadLine();
                }
            }
            InputStrings = InputStrings.Select(k => new string(k.Reverse().ToArray())).ToArray();
            InputStrings = Radix(InputStrings, ArrayOfParams[1], ArrayOfParams[2]);
            InputStrings = InputStrings.Select(k => new string(k.Reverse().ToArray())).ToArray();
            using (var Outfile = new StreamWriter("radixsort.out"))
            {
                foreach (string item in InputStrings)
                {
                    Console.WriteLine(item);
                }
            }
        }
        static string[] Radix(string[] InputArray, int LengthOfStrings, int CountOfSteps)
        {
            string[] OutputArray = new string[InputArray.Length];
            for (int i = 0; i < CountOfSteps; i++)
            {
                int[] Letterlist = new int[26];
                int Counter = 0;
                for (int j = 0; j < 26; j++)
                {
                    Letterlist[j] = 0;
                }
                for (int j = 0; j < InputArray.Length; j++)
                {
                    Letterlist[InputArray[j][i] - 'a']++;
                }
                for (int j = 0; j < 26; j++)
                {
                    int Helper = Letterlist[j];
                    Letterlist[j] = Counter;
                    Counter += Helper;
                }
                for (int j = 0; j < InputArray.Length; j++)
                {
                    OutputArray[Letterlist[InputArray[j][i] - 'a']] = InputArray[j];
                    Letterlist[InputArray[j][i] - 'a']++;
                }
                OutputArray.CopyTo(InputArray, 0);
            }
            return InputArray;
        }
    }
}

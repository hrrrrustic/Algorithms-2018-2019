using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Search2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("search2.in");
            string pattern = data[0];
            string text = pattern + "#" + data[1];
            int len = data[0].Length + data[1].Length + 1;
            int[] zfunc = new int[len];
            int left = 0, right = 0;
            for (int i = 1; i < len; i++)
            {
                zfunc[i] = Math.Max(0, Math.Min((right - i), zfunc[i - left]));
                while (i + zfunc[i] < len && text[zfunc[i]] == text[i + zfunc[i]])
                    zfunc[i]++;
                if(i + zfunc[i] > right)
                {
                    left = i;
                    right = i + zfunc[i];
                }
            }
            int answer = 0;
            for (int i = pattern.Length; i < len; i++)
            {
                if (zfunc[i] == pattern.Length)
                    answer++;
            }
            Console.WriteLine(answer);
            for (int i = pattern.Length; i < len; i++)
            {
                if (zfunc[i] == pattern.Length)
                    Console.Write((i - pattern.Length) + " ");
            }
        }
    }
}

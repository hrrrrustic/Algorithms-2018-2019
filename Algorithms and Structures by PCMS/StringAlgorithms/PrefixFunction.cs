using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrefixFunction
{
    class ZFunction
    {
        static void Solve(string[] args)
        {
            string data = File.ReadAllText("prefix.in").Trim();
            int len = data.Length;
            int[] zfunc = new int[len];
            int left = 0, right = 0;
            for (int i = 1; i < len; i++)
            {
                zfunc[i] = Math.Max(0, Math.Min((right - i), zfunc[i - left]));
                while (i + zfunc[i] < len && data[zfunc[i]] == data[i + zfunc[i]])
                    zfunc[i]++;
                if (i + zfunc[i] > right)
                {
                    left = i;
                    right = i + zfunc[i];
                }
            }
            int[] pfunc = new int[zfunc.Length];
            for (int i = 1; i < zfunc.Length; i++)
            {
                for (int j = zfunc[i] - 1; j > -1; j--)
                    if (pfunc[i + j] > 0)
                        break;
                    else
                        pfunc[i + j] = j + 1;
            }
            Console.WriteLine(string.Join(" ", pfunc));
        }
    }
}

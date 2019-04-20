using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Brackets
{
    class Brackets
    {
        static void Main(string[] args)
        {
            List<string> Answers = new List<string>();
            string[] input = File.ReadAllLines("brackets.in");
            for (int i = 0; i < input.Length; i++)
            {
                int LengthHelper = input[i].Length;
                for (int j = 1; j < LengthHelper; j++)
                {
                    if (j < 1)
                        j = 1;
                    if (input[i].Length > 1)
                    {
                        if (input[i][j - 1] + 1 == input[i][j] || input[i][j - 1] + 2 == input[i][j])
                        {
                            input[i] = input[i].Remove(j - 1, 2);
                            j -= 2;
                            LengthHelper -= 2;
                        }
                    }
                }
                if (input[i] == "")
                {
                    Answers.Add("YES");
                }
                else
                {
                    Answers.Add("NO");
                }
            }
            using (var outfile = new StreamWriter("brackets.out"))
            {
                outfile.WriteLine(string.Join("\r\n", Answers));
            }
        }
    }
}
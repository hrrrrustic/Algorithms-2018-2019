using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Garland
{
    class Program
    {
        static double answer;
        static void Main(string[] args)
        {
            string[] Input;
            using (var file = new StreamReader("garland.in"))
            {
                Input = Console.ReadLine().Split();
            }
            int BulbsCount = int.Parse(Input[0]);
            Input[1] = Input[1].Replace('.', ',');
            double firstHeight = double.Parse(Input[1]);
            double[] bulbsHeight = new double[BulbsCount];
            bulbsHeight[0] = firstHeight;
            Binsearch(0.00, firstHeight, BulbsCount, bulbsHeight);
            using (var outFile = new StreamWriter("garland.out"))
            {
                int answerA = (int)answer;
                int answerB = (int)(answer * 100 % 100);
                Console.WriteLine(answerA + "." + answerB);
            }
        }
        static void Binsearch(double left, double right, int n, double[] bulbsHeight)
        {
            double mid;
            while (left < right - 0.000005)
            {
                mid = (right + left) / 2.0;
                int check = IsthisUnerZero(bulbsHeight, mid, n);

                if (check == -1)
                {
                    left = mid;
                }
                else
                {
                    right = mid;
                }
            }
        }
        static int IsthisUnerZero(double[] bulbsHeight, double m, int n)
        {
            int check = 1;
            bulbsHeight[1] = m;
            for (int i = 2; i < n; i++)
            {
                bulbsHeight[i] = 2.0 * bulbsHeight[i - 1] + 2.0 - bulbsHeight[i - 2];
                if (bulbsHeight[i] < 0)
                {
                    check = -1;
                    break;
                }
            }
            if (check == 1)
            {
                answer = Math.Floor(bulbsHeight[n - 1] * 100) / 100;
            }

            return check;
        }
    }
}

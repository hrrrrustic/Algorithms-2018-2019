using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Binary_Search
{
    class Program
    {
        static void Main()
        {
            int MassiveSize;
            int[] Data;
            int RequestCount;
            int[] Requests;
            List<int> Answers = new List<int>();
            using (var Console = new StreamReader("binsearch.in "))
            {
            MassiveSize = int.Parse(Console.ReadLine());
            Data = Console.ReadLine().Split(' ').Select(n => int.Parse(n)).ToArray();
            RequestCount = int.Parse(Console.ReadLine());
            Requests = Console.ReadLine().Split(' ').Select(n => int.Parse(n)).ToArray();
            }
            for (int i = 0; i < RequestCount; i++)
            {
                Answers.Add(binSearch1(Data, Requests[i]));
                Answers.Add(binSearch2(Data, Requests[i]));
            }
            using (var outConsole = new StreamWriter("binsearch.out"))
            {
            for (int i = 0; i < Answers.Count; i += 2)
            {
                Console.WriteLine(Answers[i] + " " + Answers[i + 1]);
            }
            }
        }
        static int binSearch1(int[] a, int val)
        {
            int left = -1;
            int right = a.Length;
            if (val > a[right - 1] || val < a[left + 1])
                return -1;
            while (left < right - 1)
            {
                int mid = (left + right) / 2;
                if (a[mid] < val)
                {
                    left = mid;
                }
                else
                {
                    right = mid;
                }
            }
            if (a[right] == val)
            {
                return right + 1;
            }
            else
            {
                return -1;
            }
        }
        static int binSearch2(int[] a, int val)
        {
            int left = -1;
            int right = a.Length;
            if (val > a[right - 1] || val < a[left + 1])
                return -1;
            while (left < right - 1)
            {
                int mid = (left + right) / 2;
                if (a[mid] <= val)
                {
                    left = mid;
                }
                else
                {
                    right = mid;
                }
            }
            if (a[right - 1] == val)
            {
                return right;
            }
            else
            {
                return -1;
            }
        }
    }
}
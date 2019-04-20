using System.IO;
using System;

namespace kthOrderStatistic
{
    class QuickSort
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        public static int KthOrdered(int[] a, int k)
        {
            int l = 0, r = a.Length - 1;
            while (true)
            {
                if (l + 1 >= r)
                {
                    if (l + 1 == r && a[l] > a[r])
                    {
                        Swap(ref a[l], ref a[r]);
                    }
                    return a[k];
                }

                int mid = (l + r) / 2;
                Swap(ref a[mid], ref a[l + 1]);
                if (a[l] > a[r])
                    Swap(ref a[l], ref a[r]);
                if (a[l + 1] > a[r])
                    Swap(ref a[l + 1], ref a[r]);
                if (a[l] > a[l + 1])
                    Swap(ref a[l], ref a[l + 1]);

                int i = l + 1;
                int j = r;
                int value = a[i];

                while (true)
                {
                    do
                    {
                        i++;
                    } while (a[i] < value);
                    do
                    {
                        j--;
                    } while (a[j] > value);
                    if (i > j)
                        break;
                    Swap(ref a[i], ref a[j]);
                }

                a[l + 1] = a[j];
                a[j] = value;

                if (j >= k) r = j - 1;
                if (j <= k) l = i;
            }
        }

        static void Solve()
        {
            int n, k;
            string[] list;
            using (var file = new StreamReader("input.txt"))
            {

                list = file.ReadLine()
                              .Split(' ');
                n = int.Parse(list[0]);
                k = int.Parse(list[1]);
                list = file.ReadLine()
                              .Split();
            }
            int A = int.Parse(list[0]);
            int B = int.Parse(list[1]);
            int C = int.Parse(list[2]);
            int[] a = new int[n];
            a[0] = int.Parse(list[3]);
            if (n > 1)
            {
                a[1] = int.Parse(list[4]);
                for (int i = 0; i < n - 2; i++)
                    a[i + 2] = A * a[i] + B * a[i + 1] + C;
            }

            Console.WriteLine(KthOrdered(a, k - 1));
            Console.ReadKey();
        }
    }
}
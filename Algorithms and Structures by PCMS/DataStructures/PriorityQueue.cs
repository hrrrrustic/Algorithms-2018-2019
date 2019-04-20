using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Priority_Queue
{
    class Program
    {
        static List<Tuple<int, int>> heap = new List<Tuple<int, int>>();
        static string[] input;
        static List<string> Answers = new List<string>();
        static void Main(string[] args)
        {
            input = File.ReadAllLines("priorityqueue.in");
            for (int i = 0; i < input.Length; i++)
            {
                string[] dataRow = input[i].Split(' ');
                switch (dataRow[0])
                {
                    case "push":
                        heap.Add(Tuple.Create(i, int.Parse(dataRow[1])));
                        siftUp(heap.Count - 1);
                        break;
                    case "extract-min":
                        if (heap.Count != 0)
                        {
                            Answers.Add(heap[0].Item2.ToString());
                            heap[0] = heap[heap.Count - 1];
                            heap.RemoveAt(heap.Count - 1);
                            siftDown(0);
                        }
                        else
                        {
                            Answers.Add("*");
                        }
                        break;
                    case "decrease-key":
                        int x = findPosition(int.Parse(dataRow[1]) - 1);
                        heap[x] = Tuple.Create(heap[x].Item1, int.Parse(dataRow[2]));
                        siftUp(x);
                        break;
                }
            }
            using (var OutFile = new StreamWriter("priorityqueue.out"))
            {
                Console.WriteLine(string.Join("\r\n", Answers));
            }
        }

        static void siftUp(int i)
        {
            while (i > 0 && heap[i].Item2 < heap[(i - 1) / 2].Item2)
            {
                Tuple<int, int> swapHelper = heap[i];
                heap[i] = heap[(i - 1) / 2];
                heap[(i - 1) / 2] = swapHelper;
                i = (i - 1) / 2;
            }
        }
        static void siftDown(int i)
        {
            while (2 * i + 1 < heap.Count)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int j = left;
                if (right < heap.Count && heap[right].Item2 < heap[left].Item2)
                    j = right;
                if (heap[i].Item2 <= heap[j].Item2)
                {
                    break;
                }
                Tuple<int, int> swapHelper = heap[i];
                heap[i] = heap[j];
                heap[j] = swapHelper;
                i = j;
            }
        }
        static int findPosition(int a)
        {
            int position = 0;
            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i].Item1 == a)
                {
                    position = i;
                    break;
                }
            }
            return position;
        }
    }
}

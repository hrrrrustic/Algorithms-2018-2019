using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.DataStructures
{
    public class Heap
    {
        public static void Solve(string[] args)
        {
            List<string> answers = new List<string>();
            List<Tuple<int, int>> heap = new List<Tuple<int, int>>();
            string[] inputRequests = File.ReadAllLines("priorityqueue.in");
            for (int i = 0; i < inputRequests.Length; i++)
            {
                string[] currentRequest = inputRequests[i].Split(' ');
                string command = currentRequest[0];
                switch (command)
                {
                    case "push":
                        Push(i, int.Parse(currentRequest[1]), heap);
                        break;
                    case "extract-min":
                        ExtractMin(heap);
                        break;
                    case "decrease-key":
                        int index = int.Parse(currentRequest[1]) - 1;
                        int value = int.Parse(currentRequest[2]);
                        DecreaseKey(index, value, heap);
                        break;
                }
            }
                Console.WriteLine(string.Join("\r\n", answers));
        }

        private static void DecreaseKey(int index, int value, List<Tuple<int,int>> heap)
        {
            int x = FindPosition(index, heap);
            heap[x] = Tuple.Create(heap[x].Item1, value);
            SiftUp(x, heap);
        }
        private static void Push(int index, int value, List<Tuple<int,int>> heap)
        {
            heap.Add(Tuple.Create(index, value));
            SiftUp(heap.Count - 1, heap);
        }
        private static string ExtractMin(List<Tuple<int,int>> heap)
        {
            string answer;
            if (heap.Count != 0)
            {
                answer = heap[0].Item2.ToString();
                heap[0] = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);
                SiftDown(0, heap);
                return answer;
            }
            else
            {
                return "*";
            }
        }
        private static void SiftUp(int index, List<Tuple<int,int>> heap)
        {
            while (index > 0 && heap[index].Item2 < heap[(index - 1) / 2].Item2)
            {
                Tuple<int, int> swapHelper = heap[index];
                heap[index] = heap[(index - 1) / 2];
                heap[(index - 1) / 2] = swapHelper;
                index = (index - 1) / 2;
            }
        }
        private static void SiftDown(int index, List<Tuple<int,int>> heap)
        {
            while (2 * index + 1 < heap.Count)
            {
                int leftPosition = 2 * index + 1;
                int rightPosition = 2 * index + 2;
                int helpPosition = leftPosition;
                if (rightPosition < heap.Count && heap[rightPosition].Item2 < heap[leftPosition].Item2)
                    helpPosition = rightPosition;
                if (heap[index].Item2 <= heap[helpPosition].Item2)
                {
                    break;
                }
                Tuple<int, int> swapHelper = heap[index];
                heap[index] = heap[helpPosition];
                heap[helpPosition] = swapHelper;
                index = helpPosition;
            }
        }
        private static int FindPosition(int neededIndex, List<Tuple<int,int>> heap)
        {
            int currentPosition = 0;
            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i].Item1 == neededIndex)
                {
                    currentPosition = i;
                    break;
                }
            }
            return currentPosition;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.BinarySearch
{
    public class BinarySearch
    {
        public static void Solve()
        {
            int arraySize = int.Parse(Console.ReadLine());
            int[] inputArray = Console
                .ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            int requestCount = int.Parse(Console.ReadLine());
            int[] requests = Console
                .ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            List<int> answers = new List<int>();
            for (int i = 0; i < requestCount; i++)
            {
                answers.Add(BinSearch(inputArray, requests[i], true));
                answers.Add(BinSearch(inputArray, requests[i], false));
            }

            for (int i = 0; i < answers.Count; i += 2)
            {
                Console.WriteLine($"{answers[i]} {answers[i + 1]}");
            }
        }

        private static int BinSearch(int[] inputArray, int valueToSearch, bool needLeftEnter)
        {
            const int notFound = -1;

            int leftPosition = -1;
            int rightPosition = inputArray.Length;
            if (valueToSearch > inputArray[rightPosition - 1] || valueToSearch < inputArray[leftPosition + 1])
                return notFound;

            while (leftPosition < rightPosition - 1)
            {
                int midPosition = (leftPosition + rightPosition) / 2;
                if (needLeftEnter ? inputArray[midPosition] < valueToSearch : inputArray[midPosition] <= valueToSearch)
                {
                    leftPosition = midPosition;
                }
                else
                {
                    rightPosition = midPosition;
                }
            }

            if (needLeftEnter ? inputArray[rightPosition] == valueToSearch : inputArray[rightPosition - 1] == valueToSearch)
            {
                return needLeftEnter ? rightPosition + 1 : rightPosition;
            }

            return notFound;
        }
    }
}
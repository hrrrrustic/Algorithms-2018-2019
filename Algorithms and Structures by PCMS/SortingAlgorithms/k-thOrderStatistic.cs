using System.IO;
using System;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    //TODO:7 И тут тоже не совпадает класс нейм и файл нейм
    public class QuickSortForStatistic
    {
        private static void Swap<T>(ref T firstElement, ref T secondElement)
        {
            T swapHelper = firstElement;
            firstElement = secondElement;
            secondElement = swapHelper;
        }

        private static int KthOrdered(int[] inputArray, int searchPosition)
        {
            int leftPosition = 0, rightPosition = inputArray.Length - 1;
            while (true)
            {
                if (leftPosition + 1 >= rightPosition)
                {
                    if (leftPosition + 1 == rightPosition && inputArray[leftPosition] > inputArray[rightPosition])
                    {
                        Swap(ref inputArray[leftPosition], ref inputArray[rightPosition]);
                    }
                    return inputArray[searchPosition];
                }

                int mid = (leftPosition + rightPosition) / 2;
                Swap(ref inputArray[mid], ref inputArray[leftPosition + 1]);
                if (inputArray[leftPosition] > inputArray[rightPosition])
                    Swap(ref inputArray[leftPosition], ref inputArray[rightPosition]);

                if (inputArray[leftPosition + 1] > inputArray[rightPosition])
                    Swap(ref inputArray[leftPosition + 1], ref inputArray[rightPosition]);

                if (inputArray[leftPosition] > inputArray[leftPosition + 1])
                    Swap(ref inputArray[leftPosition], ref inputArray[leftPosition + 1]);

                int i = leftPosition + 1;
                int j = rightPosition;

                int value = inputArray[i];

                while (true)
                {
                    do
                    {
                        i++;
                    } while (inputArray[i] < value);
                    do
                    {
                        j--;
                    } while (inputArray[j] > value);
                    if (i > j)
                    {
                        break;
                    }

                    Swap(ref inputArray[i], ref inputArray[j]);
                }

                inputArray[leftPosition + 1] = inputArray[j];
                inputArray[j] = value;

                if (j >= searchPosition)
                {
                    rightPosition = j - 1;
                }
                if (j <= searchPosition)
                {
                    leftPosition = i;
                }
            }
        }

        public static void Solve()
        {
            int[][] inputData = File.ReadAllLines("kth.in").Select(k => k.Trim().Split(' ').Select(e => int.Parse(e)).ToArray()).ToArray();
            int arrayLength = inputData[0][0];
            int searchPosition = inputData[0][1];
            int AValue = inputData[1][0];
            int BValue = inputData[1][1];
            int CValue = inputData[1][2];
            int firstElemnet = inputData[1][3];
            int secondElement = inputData[1][4];
            int[] arrayForSearching = FillArray(arrayLength, AValue, BValue, CValue, firstElemnet, secondElement);

            Console.WriteLine(KthOrdered(arrayForSearching, searchPosition - 1));
        }

        //TODO:8 Кодстайл
        private static int[] FillArray(int arrayLength, int A, int B, int C, int firstElement, int secondelement)
        {
            int[] filledArray = new int[arrayLength];
            filledArray[0] = firstElement;
            if (arrayLength > 1)
            {
                filledArray[1] = secondelement;
            }
            for (int i = 0; i < arrayLength - 2; i++)
            {
                filledArray[i + 2] = A * filledArray[i] + B * filledArray[i + 1] + C;
            }
            return filledArray;
        }
    }
}
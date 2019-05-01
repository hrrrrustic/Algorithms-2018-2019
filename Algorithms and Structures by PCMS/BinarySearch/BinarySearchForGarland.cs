using System;

namespace AlgorithmsAndStructuresByPCMS.BinarySearch
{
    public class BinarySearchForGarland
    {
        //Correct algorithm, but problem with rounding 
        public static void Solve()
        {
            string[] inputData = Console.ReadLine().Split(' ');
            int bulbsCount = int.Parse(inputData[0]);
            double firstHeight = double.Parse(inputData[1].Replace('.', ','));
            double[] bulbsHeight = new double[bulbsCount];
            bulbsHeight[0] = firstHeight;

            double answer = BinarySearchSecondBulbHeight(0.00, firstHeight, bulbsCount, bulbsHeight);

            Console.WriteLine(answer);
        }

        private static double BinarySearchSecondBulbHeight(double leftPosition, double rightPosition, int bulbsCount, double[] bulbsHeight)
        {
            double midPosition = 0;
            while (leftPosition < rightPosition - 0.000005)
            {
                midPosition = (rightPosition + leftPosition) / 2.0;
                if (IsItValid(bulbsHeight, midPosition, bulbsCount))
                {
                    rightPosition = midPosition;
                }
                else
                {
                    leftPosition = midPosition;
                }
            }
            return midPosition;
        }

        private static bool IsItValid(double[] bulbsHeight, double secondBulbHeight, int bulbsCount)
        {
            bulbsHeight[1] = secondBulbHeight;
            for (int i = 2; i < bulbsCount; i++)
            {
                bulbsHeight[i] = 2.0 * bulbsHeight[i - 1] + 2.0 - bulbsHeight[i - 2];
                if (bulbsHeight[i] < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

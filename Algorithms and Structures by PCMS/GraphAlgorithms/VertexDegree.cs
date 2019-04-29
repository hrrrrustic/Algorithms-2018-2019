using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class VertexDegree
    {
        public static void Solve()
        {
            
            string[] inputData = File.ReadAllLines("input.txt");
            int vertexCount = int.Parse(inputData[0].Split(' ').First());
            File.WriteAllText("output.txt", string.Join(" ", VertexDegrees(inputData.Skip(1).ToArray(), vertexCount)));
        }
        private static int[] VertexDegrees(string[] edgeList, int verteCount)
        {
            int[] degrees = new int[verteCount];
            for (int i = 0; i < edgeList.Length; i++)
            {
                int[] splittedData = edgeList[i].Split(' ').Select(k => int.Parse(k) - 1).ToArray();
                degrees[splittedData[0]]++;
                degrees[splittedData[1]]++;
            }
            return degrees;
        }
    }
}

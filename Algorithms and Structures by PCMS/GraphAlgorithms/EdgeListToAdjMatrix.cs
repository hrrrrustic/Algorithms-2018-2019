using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class MatrixForEdjeToMatrix
    {
        public static void Solve()
        {
            string[] inputData = File.ReadAllLines("input.txt");
            int vertexCount = int.Parse(inputData[0].Split(' ').First());
            int edgeCount = int.Parse(inputData[0].Split(' ').Last());
            int[,] matrix = EdgeListToMatrix(vertexCount, edgeCount, inputData.Skip(1).ToArray());
            OutPut(matrix, vertexCount);
        }
        private static int[,] EdgeListToMatrix(int vertexCount,int edgeCount, string[] edjeList)
        {
            int[,] adjMatrix = new int[vertexCount, vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                int j = int.Parse(edjeList[i].Split(' ').First()) - 1;
                int k = int.Parse(edjeList[i].Split(' ').Last()) - 1;
                adjMatrix[j, k] = 1;
            }
            return adjMatrix;
        }
        private static void OutPut(int[,] adjMatrix, int vertexCount)
        {
            using (var outFile = new StreamWriter("output.txt"))
            {
                for (int i = 0; i < vertexCount; i++)
                {
                    for (int j = 0; j < vertexCount; j++)
                    {
                        outFile.Write(adjMatrix[i, j] + " ");
                    }
                    outFile.Write("\r\n");
                }
            }
        }
    }
}

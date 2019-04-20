using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IsUndirected
{
    class Program
    {
        static void Main(string[] args)
        {
            bool unDirected = true;
            string[] data = File.ReadAllLines("input.txt");
            int vertexCount = int.Parse(data[0]);
            string[,] matrix = new string[vertexCount, vertexCount];
            for (int i = 0; i < data.Length - 1; i++)
            {
                string[] splittedData = data[i + 1].Split(' ');
                for (int j = 0; j < vertexCount; j++)
                {
                    matrix[i, j] = splittedData[j];
                }
            }
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    if (matrix[i, j] != matrix[j, i] || (i == j && matrix[i,j] == "1"))
                        unDirected = false;
                }
            }
            File.WriteAllText("output.txt", unDirected == true ? "YES" : "NO");
        }
    }
}

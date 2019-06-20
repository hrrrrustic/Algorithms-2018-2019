using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{

    public class BreadthFirstSearch
    {
        static bool[] visited;
        static int[] vertexList;
        static List<int>[] adjList;

        public static void Solve(string[] args)
        {
            int[][] inputData = File
                .ReadAllLines("pathbge1.in")
                .Select(k => k.Trim().Split(' ').Select(int.Parse).ToArray())
                .ToArray();
            int vertexCount = inputData[0][0];
            int edgeCount = inputData[0][1];
            vertexList = new int[vertexCount];
            visited = new bool[vertexCount];
            adjList = new List<int>[vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                string[] splittedData = inputData[i + 1].Split(' ');
                int j = int.Parse(splittedData[0]) - 1;
                int k = int.Parse(splittedData[1]) - 1;
                if (adjList[j] == null)
                    adjList[j] = new List<int>();
                adjList[j].Add(k);
                if (adjList[k] == null)
                    adjList[k] = new List<int>();
                adjList[k].Add(j);
            }
            BFS(0, vertexCount);
            string answer = string.Join(" ", vertexList);
            Console.WriteLine(answer);
        }

        private static void BFS(int startVertex, int vertexCount)
        {
            Queue<int> dfsqueue = new Queue<int>();
            dfsqueue.Enqueue(startVertex);
            visited[startVertex] = true;
            vertexList[startVertex] = 0;
            while (dfsqueue.Count != 0)
            {
                int curr = dfsqueue.Dequeue();
                if (adjList[curr] != null)
                {
                    for (int i = 0; i < adjList[curr].Count; i++)
                    {
                        if (!visited[adjList[curr][i]])
                        {
                            visited[adjList[curr][i]] = true;
                            vertexList[adjList[curr][i]] = vertexList[curr] + 1;
                            dfsqueue.Enqueue(adjList[curr][i]);
                        }
                    }
                }
            }
        }
    }
}

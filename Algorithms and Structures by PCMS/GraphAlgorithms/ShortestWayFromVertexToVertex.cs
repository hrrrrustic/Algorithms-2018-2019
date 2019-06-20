using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{

    public class DijkstraForWayToVertex
    {
        public static void Solve(string[] args)
        {
            long[][] data = File
                .ReadAllLines("pathmgep.in")
                .Select(k => k.Trim().Split(' ').Select(long.Parse).ToArray())
                .ToArray();

            long vertexCount = data[0][0];
            long start = data[0][1] - 1;
            long finish = data[0][2] - 1;
            List<KeyValuePair<long, long>>[] adjMatrix = new List<KeyValuePair<long, long>>[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    if (i == j)
                        continue;
                    if (data[i + 1][j] != -1)
                    {
                        if (adjMatrix[i] == null)
                            adjMatrix[i] = new List<KeyValuePair<long, long>>();
                        adjMatrix[i].Add(new KeyValuePair<long, long>(j, data[i + 1][j]));
                    }
                }
            }
            long[] dist = new long[vertexCount];
            bool[] used = new bool[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                dist[i] = long.MaxValue;
            }
            dist[start] = 0;
            for (int i = 0; i < vertexCount; i++)
            {
                long q = -1;
                for (int j = 0; j < vertexCount; j++)
                {
                    if (!used[j] && (q == -1 || dist[j] < dist[q]))
                        q = j;
                }
                used[q] = true;
                if (adjMatrix[q] != null)
                    foreach (var item in adjMatrix[q])
                    {
                        if (dist[q] + item.Value < dist[item.Key])
                            dist[item.Key] = dist[q] + item.Value;
                    }
            }

            if (dist[finish] == long.MaxValue)
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(dist[finish]);
            }
        }
    }
}

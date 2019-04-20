using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ShortestWayFromVertexToVertex
{

    public class KVPComparer : IComparer<KeyValuePair<long, long>> // Господи за что?
    {
        public int Compare(KeyValuePair<long, long> kvp1, KeyValuePair<long, long> kvp2)
        {
            if (kvp1.Value < kvp2.Value)
                return -1;
            else if (kvp1.Value > kvp2.Value)
                return 1;
            else if (kvp1.Value == kvp2.Value && kvp1.Key == kvp2.Key)
                return 0;
            else if (kvp1.Key > kvp2.Key && kvp1.Value == kvp2.Value)
                return 1;
            else return -1;
        }
    }
    class Dijkstra
    {
        static void Solve(string[] args)
        {
            long[][] data = File.ReadAllLines("pathmgep.in").Select(k => k.Trim().Split(' ').Select(e => long.Parse(e)).ToArray()).ToArray();
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

            //File.WriteAllText("pathmgep.out", dist[finish].ToString());
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

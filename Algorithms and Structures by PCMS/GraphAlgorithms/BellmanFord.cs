using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GraphAlgorithms
{
    class BellmanFord
    {
        static void Solve(string[] args)
        {
            long[][] data = File.ReadAllLines("path.in").Select(k => k.Trim().Split(' ').Select(e => long.Parse(e)).ToArray()).ToArray();
            long vertexCount = data[0][0];
            long edgeCount = data[0][1];
            long start = data[0][2] - 1;
            List<Tuple<long, long, long>> edgeList = new List<Tuple<long, long, long>>();
            for (int i = 0; i < edgeCount; i++)
            {
                long from = data[i + 1][0] - 1;
                long to = data[i + 1][1] - 1;
                long weight = data[i + 1][2];
                edgeList.Add(Tuple.Create(from, to, weight));
            }
            long[] dist = new long[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                dist[i] = long.MaxValue;
            }
            dist[start] = 0;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                for (int j = 0; j < edgeCount; j++)
                {
                    long from = edgeList[j].Item1;
                    long to = edgeList[j].Item2;
                    long w = edgeList[j].Item3;
                    if (dist[from] != long.MaxValue && dist[from] + w < dist[to])
                        dist[to] = dist[from] + w;
                }
            }
            for (int i = 0; i < vertexCount - 1; i++)
            {
                for (int j = 0; j < edgeCount; j++)
                {

                    long from = edgeList[j].Item1;
                    long to = edgeList[j].Item2;
                    long w = edgeList[j].Item3;
                    if (dist[from] != long.MaxValue && dist[from] + w < dist[to])
                        dist[to] = (long)(-5*1e18) + 228;
                }
            }
            for (int i = 0; i < vertexCount; i++)
            {
                if (dist[i] <= -5 * 1e18 + 228) // 18 нулей
                    Console.WriteLine("-");
                else if (dist[i] == long.MaxValue)
                    Console.WriteLine("*");
                else
                    Console.WriteLine(dist[i]);
            }
        }

    }
}

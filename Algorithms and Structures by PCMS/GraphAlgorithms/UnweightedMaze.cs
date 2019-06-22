using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class DepthFirstSearchForMaze
    {
        private const int Start = 0;
        private const int Finish = 10001;
        private const int Wall = -1;
        private const int Road = -2;

        private class Maze
        {
            public int[][] MazeMap { get; }
            public int RowCount { get; }
            public int ColumnCount { get; }
            public Tuple<int,int> StartPosition { get; }
            public Tuple<int,int> FinishPosition { get; set; }

            public Maze(int[][] maze, int rowCount, int columnCount, Tuple<int,int> startPosition, Tuple<int,int> finishPosition)
            {
                MazeMap = maze;
                RowCount = rowCount;
                ColumnCount = columnCount;
                StartPosition = startPosition;
                FinishPosition = finishPosition;
            }
        }
        public static void Solve(string[] args)
        {
            Maze currentMaze = InitMaze();
            
            bool wayExisted = BFSCheckWay(currentMaze);

            List<char> commands = wayExisted ? FindShortestWay(currentMaze) : null;

            using (var outFile = new StreamWriter("output.txt"))
            {
                if (commands == null)
                {
                    outFile.WriteLine(-1);
                }
                else
                {
                    commands.Reverse();
                    outFile.WriteLine(commands.Count);
                    outFile.Write(string.Join("", commands));
                }
            }
        }

        private static Maze InitMaze()
        {
            using (var file = new StreamReader("input.txt"))
            {
                string[] mazeInfo = file.ReadLine().Split(' ');
                int rowCount = int.Parse(mazeInfo[0]);
                int colCount = int.Parse(mazeInfo[1]);
                Tuple<int, int> startPosition = null;
                Tuple<int, int> finishPosition = null;
                int[][] data = Enumerable.Repeat(new int[colCount], rowCount).ToArray();

                for (int i = 0; i < rowCount; i++)
                {
                    char[] currentMazeRow = file.ReadLine().ToCharArray();
                    for (int j = 0; j < colCount; j++)
                    {
                        switch (currentMazeRow[j])
                        {
                            case '.':
                                data[i][j] = Road;
                                break;
                            case 'T':
                                finishPosition = new Tuple<int, int>(i,j);
                                data[i][j] = Finish;
                                break;
                            case 'S':
                                startPosition = new Tuple<int, int>(i,j);
                                data[i][j] = Start;
                                break;
                            case '#':
                                data[i][j] = Wall;
                                break;
                        }

                    }
                }
                return new Maze(data, rowCount,colCount, startPosition, finishPosition);
            }
        }
        private static bool BFSCheckWay(Maze maze)
        {
            Queue<Tuple<int,int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(maze.StartPosition);

            while (queue.Count != 0)
            {
                Tuple<int,int> currentPosition = queue.Dequeue();
                int currentRow = currentPosition.Item1;
                int currentColumn = currentPosition.Item2;

                for (int i = currentRow - 1; i < currentRow + 2; i++)
                {
                    for (int j = currentColumn - 1; j < currentColumn + 2; j++)
                    {
                        if (!IsItValidNeighbour(maze.ColumnCount, maze.RowCount, j, i, currentColumn, currentRow))
                            continue;
                        if (maze.MazeMap[i][j] == Road)
                        {
                            queue.Enqueue(new Tuple<int, int>(i, j));
                            maze.MazeMap[i][j] = maze.MazeMap[currentRow][currentColumn] + 1;
                        }
                        if (maze.MazeMap[i][j] == Finish)
                        {
                            maze.FinishPosition = new Tuple<int, int>(i, j);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool IsItValidNeighbour(int maxColumn, int maxRow, int j, int i, int currentCol, int currentRow)
        {
            if (Math.Abs(currentCol - j) + Math.Abs(currentRow - i) != 1 || j > maxColumn - 1 || j < 0 ||
                i > maxRow - 1 || i < 0) return false;

            return true;
        }

        private static List<char> FindShortestWay(Maze maze)
        {
            List<char> commands = new List<char>();
            int finishRowPosition = maze.FinishPosition.Item1;
            int finishColumnPosition = maze.FinishPosition.Item2;
            int minDistanceNeighbour = 10000;
            int minNeighbourCol = 0;
            int minNeighbourRow = 0;

            while (minDistanceNeighbour != 0)
            {
                for (int i = finishRowPosition - 1; i < finishRowPosition + 2; i++)
                {
                    for (int j = finishColumnPosition - 1; j < finishColumnPosition + 2; j++)
                    {
                        if (!IsItValidNeighbour(maze.ColumnCount, maze.RowCount, j, i, finishColumnPosition, finishRowPosition) 
                            || maze.MazeMap[i][j] == Wall)
                            continue;
                        if (maze.MazeMap[i][j] < minDistanceNeighbour)
                        {
                            minDistanceNeighbour = maze.MazeMap[i][j];
                            minNeighbourRow = i;
                            minNeighbourCol = j;
                        }
                    }
                }
                if (minNeighbourRow > finishRowPosition)
                    commands.Add('U');
                else if (minNeighbourRow < finishColumnPosition)
                    commands.Add('D');
                else if (minNeighbourCol > finishColumnPosition)
                    commands.Add('L');
                else commands.Add('R');

                finishRowPosition = minNeighbourRow;
                finishColumnPosition = minNeighbourCol;
            }

            return commands;
        }
    }
}
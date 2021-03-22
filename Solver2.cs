using System;
using System.Collections.Generic;

namespace RandSudoku
{
    class HashSolver2
    {
        static HashSet<char>[] rows = new HashSet<char>[9];
        static HashSet<char>[] cols = new HashSet<char>[9];
        static HashSet<char>[] boxs = new HashSet<char>[9];

        public static void SolveSudoku(char[][] board)
        {
            /*
            char[][] sudoku = new char[9][];
            for (int i = 0; i < 9; i++)
                sudoku[i] = new char[9];

            sudoku = LoadSudoku(sudokuStr);
            */

            FillHashSets(board);

            /*
            PrintSet(rows);
            PrintSet(cols);
            PrintSet(boxs);
            */

            Solve(board, 0, 0);
        }

        public static void PrintSudoku(char[][] board)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0 && i != 0)
                    Console.WriteLine();

                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0)
                        Console.Write('\t');

                    Console.Write($"{board[i][j]} ");
                }
                Console.WriteLine();
            }
        }

        static void PrintSet(HashSet<char>[] hashArray)
        {
            foreach (HashSet<char> set in hashArray)
            {
                foreach (char c in set)
                    Console.Write($"{c} ");

                Console.WriteLine();
            }
        }

        static bool Solve(char[][] board, int i, int j)
        {
            for (int k = i; k < 9; k++)
            {
                for (int l = j; l < 9; l++)
                {
                    if(board[k][l] != '0')
                        continue;

                    for (int n = 49; n <= 57; n++)
                    {
                        char num = (char)n;

                        if (!rows[k].Contains(num) &&
                                !cols[l].Contains(num) &&
                                !boxs[(k / 3) * 3 + (l / 3)].Contains(num))
                        {
                            board[k][l] = num;
                            rows[k].Add(num);
                            cols[l].Add(num);
                            boxs[(k / 3) * 3 + (l / 3)].Add(num);

                            if (Solve(board, k, l))
                                return true;

                            board[k][l] = '0';

                            rows[k].Remove(num);
                            cols[l].Remove(num);
                            boxs[(k / 3) * 3 + (l / 3)].Remove(num);
                        }
                    }

                    if (board[k][l] == '0')
                        return false;
                }
                j = 0;
            }
            return true;
        }

        static char[][] LoadSudoku(string board)
        {
            char[][] newBoard = new char[9][]
            {
                new char[9], new char[9], new char[9],
                new char[9], new char[9], new char[9],
                new char[9], new char[9], new char[9]
            };

            int x = 0;
            for(int i = 0; i < 9; i++)
                for(int j = 0; j < 9; j++)
                {
                    newBoard[i][j] = board[x];
                    Console.WriteLine($"{newBoard[i][j]} {board[x]}");
                    
                    x++;
                }

            return newBoard;
        }

        static void FillHashSets(char[][] board)
        {
            for (int i = 0; i < 9; i++)
            {
                rows[i] = new HashSet<char>(9);
                cols[i] = new HashSet<char>(9);
                boxs[i] = new HashSet<char>(9);
            }

            foreach (HashSet<char> row in rows)
                row.Clear();
            foreach (HashSet<char> col in cols)
                col.Clear();
            foreach (HashSet<char> box in boxs)
                box.Clear();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] == '0')
                        continue;

                    int s = (i / 3) * 3 + (j / 3);

                    rows[i].Add(board[i][j]);
                    cols[j].Add(board[i][j]);
                    boxs[s].Add(board[i][j]);
                }
            }
        }
    }
}

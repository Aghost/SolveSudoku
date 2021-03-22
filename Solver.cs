using SudokuSolver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SudokuSolver.Logics
{
	public class Solver
	{
		public int[][] Solve(int[][] sudoku)
		{
			int[][] solved = sudoku;

			SolveSudoku(solved);

			return (solved);
		}

		static bool SolveSudoku(int[][] board)
		{
			int row = -1;
			int col = -1;
			bool isEmpty = true;

			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					if (board[i][j] == 0)
					{
						row = i;
						col = j;

						isEmpty = false;
						break;
					}

					if (!isEmpty)
						break;
				}
			}

			if (isEmpty)    //no 0s left
				return true;

			Random r = new Random();
			int[] shuffled = Enumerable.Range(1, 9).Reverse().OrderBy(r.Next).ToArray();

			for (int num = 1; num <= 9; num++)
			{
				if (IsSafe(board, row, col, shuffled[num - 1]))
				{
					board[row][col] = shuffled[num - 1];

					if (SolveSudoku(board))
						return true;
					else
						board[row][col] = 0;
				}
			}

			return false;
		}
    
		private static bool IsSafe(int[][] board, int row, int col, int num)
		{
			for (int i = 0; i < 9; i++)
				if (board[row][i] == num || board[i][col] == num)
					return false;

			int rStart = row - row % 3;
			int cStart = col - col % 3;

			for (int i = rStart; i < rStart + 3; i++)
				for (int j = cStart; j < cStart + 3; j++)
					if (board[i][j] == num)
						return false;

			return true;
		}

		
		
		public int[][] Create(int[][] sudoku)
    {
			return sudoku;
    }
}

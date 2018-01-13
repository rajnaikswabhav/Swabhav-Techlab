package com.techlab.tictactoe;

import com.techlabs.enums.Seed;

public class GameBoard {

	private static final int ROWS = 3;
	private static final int COLS = 3;
	public Cell[][] cells;
	private int currentRow;
	private int currentCol;

	public GameBoard() {

		cells = new Cell[ROWS][COLS];
		for (int row = 0; row < ROWS; ++row) {
			for (int col = 0; col < COLS; ++col) {
				cells[row][col] = new Cell(row, col);
			}
		}
	}

	public void inIt() {
		for (int row = 0; row < ROWS; ++row) {
			for (int col = 0; col < COLS; ++col) {
				cells[row][col].clear();
			}
		}
	}

	public boolean isDraw() {
		for (int row = 0; row < ROWS; ++row) {
			for (int col = 0; col < COLS; ++col) {
				if (cells[row][col].content == Seed.EMPTY) {
					return false;
				}
			}
		}
		return true;
	}

	public boolean hasWon(Seed theSeed) {
		if ((cells[currentRow][0].content == theSeed
				&& cells[currentRow][1].content == theSeed && cells[currentRow][2].content == theSeed)
				|| (cells[0][currentCol].content == theSeed
						&& cells[1][currentCol].content == theSeed && cells[2][currentCol].content == theSeed)
				|| (cells[0][0].content == theSeed
						&& cells[1][1].content == theSeed && cells[2][2].content == theSeed)
				|| (cells[0][2].content == theSeed
						&& cells[1][1].content == theSeed && cells[2][0].content == theSeed)) {

			return true;
		}
		return false;
	}

	public static int getRows() {
		return ROWS;
	}

	public static int getCols() {
		return COLS;
	}


	public int getCurrentRow() {
		return currentRow;
	}

	public int getCurrentCol() {
		return currentCol;
	}

	public void setCurrentRow(int currentRow) {
		this.currentRow = currentRow;
	}

	public void setCurrentCol(int currentCol) {
		this.currentCol = currentCol;
	}

}

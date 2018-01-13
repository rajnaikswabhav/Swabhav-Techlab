package com.techlab.tictactoe;

import com.techlabs.enums.Seed;

public class Cell {

	Seed content;
	int row, col;

	public Cell(int row, int col) {
		this.row = row;
		this.col = col;
		clear();
	}

	public void clear() {
		content = Seed.EMPTY;
	}

	public char printCell() {
		switch (content) {
		case CROSS:
			return 'X';
		case NOUGHT:
			return 'O';
		case EMPTY:
			return ' ';
		}
		return ' ';
	}

	/*
	 * public void printCell() { switch (content) { case CROSS:
	 * System.out.print(" X "); break; case NOUGHT: System.out.print(" O ");
	 * break; case EMPTY: System.out.print("  "); break; } }
	 */
	public Seed getContent() {
		return content;
	}

	public void setContent(Seed content) {
		this.content = content;
	}
}

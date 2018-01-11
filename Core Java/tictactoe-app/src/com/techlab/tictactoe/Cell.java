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

	public void paint() {
		switch (content) {
		case CROSS:
			System.out.println(" X ");
			break;
		case NOUGHT:
			System.out.println(" O ");
			break;
		case EMPTY:
			System.out.println("  ");
			break;
		}
	}
}

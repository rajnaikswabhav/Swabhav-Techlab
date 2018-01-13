package com.techlab.tictactoe;

import com.techlabs.enums.GameState;
import com.techlabs.enums.Seed;

public class TicTacToeGameUI {

	private GameState currentState;
	private Seed currentPlayer;
	private static final int ROWS = 3;
	private static final int COLS = 3;
	public Cell[][] cells;
	private TicTacToeGameConsole console; 

	public TicTacToeGameUI() {
		cells = new Cell[ROWS][COLS];
		for (int row = 0; row < ROWS; ++row) {
			for (int col = 0; col < COLS; ++col) {
				cells[row][col] = new Cell(row, col);
			}
		}
	}

	public void start() {

		currentPlayer = Seed.CROSS;
		currentState = GameState.PLAYING;
		console = new TicTacToeGameConsole(new GameBoard());

		do {
			console.playerMove(currentPlayer);
			printBoard();
			console.updateGame(currentPlayer);

			if (currentState == GameState.CROSS_WON) {
				System.out.println("X is Win....");
			} else if (currentState == GameState.NOUGHT_WON) {
				System.out.println("O is Win...");
			} else if (currentState == GameState.DRAW) {
				System.out.println("Game is Draw....");
			}

			currentPlayer = (currentPlayer == Seed.CROSS) ? Seed.NOUGHT
					: Seed.CROSS;
		} while (currentState == GameState.PLAYING);
	}

	public void printBoard() {
		
		for (int row = 0; row < ROWS; ++row) {
			for (int col = 0; col < COLS; ++col) {
				System.out.print(cells[row][col].printCell());
				if (col < COLS - 1) {
					System.out.print("|");
				}
			}

			System.out.println();
			if (row < row - 1) {
				System.out.println("--------------");
			}
		}
	}
}

package com.techlab.tictactoe;

import java.util.Scanner;

import com.techlabs.enums.GameState;
import com.techlabs.enums.Seed;

public class TicTacToeGameConsole {

	private GameBoard gameBoard;
	private GameState currentState;
	private Seed currentPlayer;

	Scanner scanner = new Scanner(System.in);

	public TicTacToeGameConsole() {
		gameBoard = new GameBoard();
	}

	public void start() {
		gameBoard.inIt();
		currentPlayer = Seed.CROSS;
		currentState = GameState.PLAYING;
		
		do{
			playerMove(currentPlayer);
			gameBoard.print();
			updateGame(currentPlayer);
			
			if(currentState == GameState.CROSS_WON){
				System.out.println("X is Win....");
			}else if(currentState == GameState.NOUGHT_WON){
				System.out.println("O is Win...");
			}else if(currentState == GameState.DRAW){
				System.out.println("Game is Draw....");
			}
			
			currentPlayer = (currentPlayer == Seed.CROSS) ? Seed.NOUGHT : Seed.CROSS;
		}while(currentState == GameState.PLAYING);
	}

	public void playerMove(Seed theSeed) {
		boolean valid = false;

		do {
			if (theSeed == Seed.CROSS) {
				System.out
						.println("Player 'X':Enter Your Move (row[1-3] column[1-3]): ");
			} else {
				System.out
						.println("Player 'O':Enter Your Move (row[1-3] column[1-3]): ");
			}

			int row = scanner.nextInt() - 1;
			int col = scanner.nextInt() - 1;

			if (row >= 0 && row < GameBoard.getRows() && col >= 0
					&& col < GameBoard.getCols()
					&& gameBoard.cells[row][col].content == Seed.EMPTY) {

				gameBoard.cells[row][col].content = theSeed;
				gameBoard.setCurrentRow(row);
				gameBoard.setCurrentCol(col);

				valid = true;
			} else {
				System.out.println("This move at ( " + (row + 1) + ","
						+ (col + 1) + ") is not valid...");
			}

		} while (!valid);
	}
	
	public void updateGame(Seed theSeed)
	{
		if(gameBoard.hasWon(theSeed))
		{
			if(theSeed == Seed.CROSS)
			{
				currentState = GameState.CROSS_WON;
			}else{
				currentState = GameState.NOUGHT_WON;
			}
		}else if(gameBoard.isDraw()){
			currentState = GameState.DRAW;
		}
	}
}

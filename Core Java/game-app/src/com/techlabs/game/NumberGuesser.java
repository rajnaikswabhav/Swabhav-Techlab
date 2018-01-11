package com.techlabs.game;

import java.util.Random;
import com.techlabs.enums.GameState;
import com.techlabs.enums.NumberState;

public class NumberGuesser {

	private int privateNumber;
	private GameState gameState;
	private NumberState numberState;

	public void inIt(int range) {
		Random random = new Random();
		privateNumber = random.nextInt(range);
		System.out.println("Private number is between 0 to " + range);
		System.out.println("Private number is: " + privateNumber);
	}

	public int getPrivateNumbe() {
		return privateNumber;
	}

	@SuppressWarnings("static-access")
	public void matched(int number) {
		if (privateNumber == number) {
			setGameState(gameState.END);
		} else {
			setGameState(gameState.INPROGRESS);
		}

	}

	public GameState getGameState() {
		return gameState;
	}

	public void setGameState(GameState gameState) {
		this.gameState = gameState;
	}

	public NumberState getNumberState() {
		return numberState;
	}

	public void setNumberState(NumberState numberState) {
		this.numberState = numberState;
	}

}

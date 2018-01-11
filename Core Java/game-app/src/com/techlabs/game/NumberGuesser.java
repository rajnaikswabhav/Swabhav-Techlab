package com.techlabs.game;

import java.util.Random;
import java.util.Scanner;

import com.techlabs.enums.GameState;
import com.techlabs.enums.NumberState;

public class NumberGuesser {

	private int privateNumbe;
	private GameState gameState;
	private NumberState numberState;

	public void inIt(int range) {
		Random random = new Random();
		privateNumbe = random.nextInt(range);
		System.out.println("Private number is between 0 to "+range);
		System.out.println("Private number is: "+privateNumbe);
	}

	public int getPrivateNumbe() {
		return privateNumbe;
	}

	public void matched(int number) {
		if (privateNumbe == number) {
			setGameState(gameState.END);
			setNumberState(numberState.MATCH);
		}
		
		else if(privateNumbe)
		{
			setGameState(gameState.INPROGRESS);
			setNumberState(numberState.GREATERTHAN);
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

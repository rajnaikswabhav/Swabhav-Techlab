package com.techlabs.game;

import java.util.Scanner;

import com.techlabs.enums.GameState;
import com.techlabs.enums.NumberState;

public class GameConsole {

	private int range;
	private int attempts;
	private int guessNumber;
	private NumberGuesser numberGuesser;
	private NumberState numberState;
	private int count = 1;

	public GameConsole(int range, int attempts) {

		this.range = range;
		this.attempts = attempts;
		numberGuesser = new NumberGuesser();
		numberGuesser.inIt(range);
	}

	public void start() {

		Scanner scanner = new Scanner(System.in);
		System.out.println("Guess Any Number.....");
		guessNumber = scanner.nextInt();
		if (guessNumber > range) {
			System.out.println("Please enter value only in a range....");
			start();
		}
		numberGuesser.matched(guessNumber);
		if (count < attempts) {
			compare(guessNumber);
		}

		else {
			System.out.println("You are out of attempts..Please try again ...");
			System.exit(0);
		}
		scanner.close();
	}

	public void compare(int num) {

		if (numberGuesser.getNumberState() == numberState.MATCH) {
			System.out.println("You win the game....");
			System.exit(0);
		}

		else if (numberGuesser.getNumberState() == numberState.GREATERTHAN) {
			System.out
					.println("Private number is greater than guess number...");
		}

		else if (numberGuesser.getNumberState() == numberState.LESSTHAN) {
			System.out.println("Private number is less than guess number...");
		}
		count++;
		start();
	}

	public NumberGuesser getNumberGuesser() {
		return numberGuesser;
	}

	public void setNumberGuesser(NumberGuesser numberGuesser) {
		this.numberGuesser = numberGuesser;
	}

}

package com.techlabs.player.test;

import com.techlabs.player.Player;

public class TestPlayer {

	public static void main(String[] args) {
		 CaseStudy1();
		Player player1 = new Player(101, "A", 23);
		Player player2 = new Player(101, "A", 23);
		System.out.println(player1 == player2);
		System.out.println(player1.equals(player2));

	}

	private static void CaseStudy1() {
		Player sachin = new Player(101, "Sachin", 48);
		Player virat = new Player(102, "Virat");

		System.out.println("id:" + sachin.getId());
		System.out.println("id:" + virat.getId());
		System.out.println("Name:" + sachin.getName());
		System.out.println("Age:" + virat.getAge() +"\n");

		Player elder = sachin.whoIsElder(virat);
		System.out.println("Elder Player is: " + elder.getName());

		System.out.println(sachin);
		System.out.println(virat.toString());
	}
}

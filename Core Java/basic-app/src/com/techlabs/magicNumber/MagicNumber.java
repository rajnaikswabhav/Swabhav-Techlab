package com.techlabs.magicNumber;

import java.util.Scanner;

public class MagicNumber {
	public static void main(String args[]) {
		Scanner scanner = new Scanner(System.in);
		System.out.println("Enter the number to be checked.");
		int n = scanner.nextInt();
		int sum = 0, num = n;
		while (num > 9) {
			sum = num;
			System.out.println("Sum = "+sum);
			int s = 0;
			while (sum != 0) {
				System.out.println("sum is= "+sum);
				s = s + (sum % 10);
				System.out.println("Module: "+sum%10);
				sum = sum / 10;
				System.out.println("After Devide: "+sum);
				System.out.println("s is = "+s);
			}
			num = s;
			System.out.println("num ="+s);
		}
		if (num == 1) {
			System.out.println("num ="+num);
			System.out.println(n + " is a Magic Number.");
		} else {
			System.out.println(n + " is not a Magic Number.");
		}
		scanner.close();
	}
}

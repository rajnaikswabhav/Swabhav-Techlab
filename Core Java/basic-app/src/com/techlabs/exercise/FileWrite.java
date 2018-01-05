package com.techlabs.exercise;

import java.io.*;

public class FileWrite {
	public static void main(String ar[]) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		String str;
		System.out.println("Enter lines of Text:");
		System.out.println("Enter STOP to Quit:");
		FileWriter f = new FileWriter("src/com/techlabs/exercise/Data/data.txt");
		BufferedWriter out = new BufferedWriter(f);
		str = br.readLine();
		do {
			out.write(str);
			out.newLine();
			str = br.readLine();
		} while (!str.equalsIgnoreCase("Stop"));
		out.close();
	}
}

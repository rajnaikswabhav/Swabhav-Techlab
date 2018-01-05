package com.techlabs.exercise;

import java.io.*;

public class FileRead {
	public static void main(String ar[]) {
		try {
			FileInputStream f = new FileInputStream(
					"src/com/techlabs/exercise/Data/data.txt");
			DataInputStream in = new DataInputStream(f);
			BufferedReader br = new BufferedReader(new InputStreamReader(in));
			String strLine;
			while ((strLine = br.readLine()) != null) {
				System.out.println(strLine);
			}
			in.close();
		} catch (Exception e) {
			System.out.println("error:" + e.getMessage());
		}
	}
}

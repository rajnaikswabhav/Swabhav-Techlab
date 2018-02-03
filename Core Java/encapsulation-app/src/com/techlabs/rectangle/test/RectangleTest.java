package com.techlabs.rectangle.test;

import java.io.*;
import com.techlabs.rectangle.Rectangle;

public class RectangleTest {

	public static void main(String[] args) {

		Rectangle rectangle1 = new Rectangle();
		rectangle1.setWidth(90);
		rectangle1.setHeight(10);
		rectangle1.setBordercolor("RED");

		Rectangle rectangle2 = new Rectangle();
		rectangle2.setWidth(45);
		rectangle2.setHeight(-50);
		rectangle2.setBordercolor("BLUE");

		Rectangle rectangle3 = new Rectangle();
		rectangle3.setWidth(100);
		rectangle3.setHeight(3);
		rectangle3.setBordercolor("GREEN");
		// rectangle3=null;

		printDetail(rectangle1);
		printDetail(rectangle2);
		printDetail(rectangle3);
		printDetail(new Rectangle());

		try {
			FileOutputStream fileOutputStream = new FileOutputStream(
					"D:/Techlabs/CloudRepositry/MyFirstProject/Core Java/rectangle.txt");
			ObjectOutputStream objectOutputStream = new ObjectOutputStream(
					fileOutputStream);
			objectOutputStream.writeObject(rectangle1);
			objectOutputStream.writeObject(rectangle2);
			objectOutputStream.writeObject(rectangle3);
			objectOutputStream.close();
		} catch (Exception e) {
			System.out.println("" + e.toString());
		}

		fileRead();
	}

	public static void printDetail(Rectangle rectangle) {
		System.out.println("Hashcode is:" + rectangle.hashCode());
		System.out.println("Width is: " + rectangle.getWidth());
		System.out.println("Height is: " + rectangle.getHeight());
		System.out.println("BorderColor is: " + rectangle.getBordercolor());
		System.out.println("Area of Rectangle is:" + rectangle.calculateArea()
				+ "\n\n");
	}
	
	public static void fileRead() {
		try {
			FileInputStream fileInputStream = new FileInputStream(
					"D:/Techlabs/CloudRepositry/MyFirstProject/Core Java/rectangle.ser");
			ObjectInputStream objectInputStream = new ObjectInputStream(
					fileInputStream);
			Object rec1 = objectInputStream.readObject();
			Object rec2 = objectInputStream.readObject();
			Object rec3 = objectInputStream.readObject();

			Rectangle rec11 = (Rectangle) rec1;
			Rectangle rec12 = (Rectangle) rec2;
			Rectangle rec13 = (Rectangle) rec3;

			printDetail(rec11);
			printDetail(rec12);
			printDetail(rec13);

			objectInputStream.close();

		} catch (Exception e) {
			System.out.println("error:" + e.getMessage());
		}
	}

}

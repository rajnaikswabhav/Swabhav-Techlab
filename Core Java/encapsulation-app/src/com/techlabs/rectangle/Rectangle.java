package com.techlabs.rectangle;

import java.io.Serializable;

public class Rectangle implements Serializable {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private int width;
	private int height;
	private String bordercolor;

	public void setWidth(int paramWidth) {
		int width = getValidSide(paramWidth);
		this.width = width;
	}

	public int getWidth() {
		return width;
	}

	public void setHeight(int paramHeight) {
		int height = getValidSide(paramHeight);
		this.height = height;
	}

	public int getHeight() {
		return height;
	}

	public void setBordercolor(String bordercolor) {
		String color = getValidColor(bordercolor);
		this.bordercolor = color;
	}

	public String getBordercolor() {
		return bordercolor;
	}

	public int calculateArea() {
		return (width * height);
	}

	private int getValidSide(int size) {
		if (size <= 1) {
			size = 1;
		} else if (size >= 100) {
			size = 100;
		}
		return size;
	}

	private String getValidColor(String color) {
		if (color.equalsIgnoreCase("blue") || color.equalsIgnoreCase("red")) {
			return color;
		} else if (color.equalsIgnoreCase("green")) {
			return color;
		}
		return color;
	}
}

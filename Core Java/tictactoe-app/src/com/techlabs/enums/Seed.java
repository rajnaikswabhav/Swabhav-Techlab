package com.techlabs.enums;

public enum Seed {

	EMPTY,CROSS,NOUGHT;	
	
	@Override
	public String toString()
	{
		switch (this) {
		case CROSS:  		return "Player 'X':Enter Your Move (row[1-3] column[1-3]):";
			
		case NOUGHT:		return "Player 'O':Enter Your Move (row[1-3] column[1-3]): ";

		default:
			break;
		}
		return null;
	}
}

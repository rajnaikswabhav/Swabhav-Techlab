package com.techlabs.enums;

public enum Branch {

	COMPUTER,INFORMATIONTECHNOLOGY,CIVIL,ELECTRIC,
	MECHANICAL,AUTOMOBIL,CHEMICAL,ARONOTICAL;
	
	@Override
	public String toString()
	{
		switch (this) {
		case COMPUTER:					return "Computer";
		case INFORMATIONTECHNOLOGY:		return "InformationTechnology";
		case CIVIL:						return "Civil";
		case ELECTRIC:					return "Electric";
		case MECHANICAL:				return "Mechanical";
		case AUTOMOBIL:					return "Automobil";
		case CHEMICAL:					return "Chemical";
		case ARONOTICAL:				return "Aronotical";

		default:
			break;
		}
		return null;
	}
}

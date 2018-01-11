package com.techlabs.enums;

public enum Wood {

	INDIAN_ROSEWOOD,BRAZILIAN_ROSEWOOD,MAHOGANY,MAPLE,
	COCOBOLD,CEDAR,ADRIONDACK,ALDER,SITKA;
	
	@Override
	public String toString()
	{
		switch (this) {
		case INDIAN_ROSEWOOD:		return "indian_rosewood";
		case BRAZILIAN_ROSEWOOD: 	return "brazilian_roadwood";
		case MAHOGANY:				return "mahogany";
		case MAPLE:					return "maple";
		case COCOBOLD:				return "cocobold";
		case CEDAR:					return "cedar";
		case ADRIONDACK:			return "adriondack";
		case ALDER:					return "alder";
		case SITKA:					return "stika";

		default:
			return null;
		}
	}
}

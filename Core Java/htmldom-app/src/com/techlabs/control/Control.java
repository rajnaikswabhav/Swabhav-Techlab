package com.techlabs.control;

public class Control implements IControlItem {

	private String controlName;
	private static int count=0;
	private static int flag = 0;

	public Control(String controlName) {
		this.controlName = controlName;
	}

	@Override
	public void showDetails() {
		if(count > 0){
			if(flag > count){
				System.out.println("\t\t\t"+controlName);
			}
			else{
				System.out.println("\t\t"+controlName);
				flag++;
			}
		}
		else{
			System.out.println("\t\t"+controlName);
			count++;
		}
	}

}

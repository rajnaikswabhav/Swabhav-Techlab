package com.techlabs.control;

import java.util.ArrayList;
import java.util.List;

public class ControlGroup implements IControlItem {

	private String controlGroupName;
	private List<IControlItem> items = new ArrayList<IControlItem>();
	private static int count = 0;
	private static int flag = 0;
	public ControlGroup(String controlGroupName) {
		this.controlGroupName = controlGroupName ;
	}

	public void addItem(IControlItem item) {
		items.add(item);
	}

	@Override
	public void showDetails() {
		if(count > 0)
		{
			if(flag > count){
				System.out.println("\t\t"+controlGroupName);
			}
			else{
				System.out.println("\t"+controlGroupName);
				flag++;
			}
		}
		else if(count == 0){
			System.out.println(controlGroupName);
			count++;
		}
		
		for (IControlItem item : items) {
			item.showDetails();
		}
	}

	public String getFolderName() {
		return controlGroupName;
	}

}

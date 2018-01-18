package com.techlabs.control.test;

import com.techlabs.control.Control;
import com.techlabs.control.ControlGroup;

public class TestControl {

	public static void main(String[] args) {

		ControlGroup html = new ControlGroup("HTMl");
		ControlGroup head = new ControlGroup("Head");
		ControlGroup body = new ControlGroup("Body");
		ControlGroup form = new ControlGroup("form");
		
		Control title = new Control("Title");
		Control div = new Control("div");
		Control paragraph = new Control("p");
		Control header = new Control("Header");
		Control footer = new Control("Footer");
		Control datalist = new Control("datalist");
		Control output = new Control("output");
		
		head.addItem(title);
		body.addItem(div);
		body.addItem(paragraph);
		body.addItem(header);
		body.addItem(footer);
		body.addItem(form);
		form.addItem(datalist);
		form.addItem(output);
		
		html.addItem(head);
		html.addItem(body);
		
		
		html.showDetails();
		

	}

}

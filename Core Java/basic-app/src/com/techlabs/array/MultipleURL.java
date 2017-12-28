package com.techlabs.array;

public class MultipleURL {

	public static void main(String[] url) {
		String domainName, argumentName;

		if (url.length == 0) {
			System.out.println("Please Enter any Argument..");
		} else {
			for (String str : url) {
				domainName = str.substring(str.indexOf(".") + 1,
						str.lastIndexOf("."));
				if (domainName.equals("")) {
					System.out.println("Domain Name not found...");
				} else {
					System.out.println(domainName);
				}

				argumentName = str.substring(str.indexOf("=") + 1);
				if (argumentName.equals("")) {
					System.out.println("Name not found...");
				} else {
					System.out.println(argumentName);
					System.out.println();
				}
			}
			
		}

	}
}

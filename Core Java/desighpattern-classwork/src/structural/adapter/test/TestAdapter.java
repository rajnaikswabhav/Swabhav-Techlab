package structural.adapter.test;

import structural.adapter.SwabhavQ;

public class TestAdapter {

	public static void main(String[] args) {

		SwabhavQ<String> swabhavQ = new SwabhavQ<String>();
		swabhavQ.enqueue("10");
		swabhavQ.enqueue("5");
		swabhavQ.enqueue("12");
		swabhavQ.enqueue("15");

		System.out.println("After Deque: " + swabhavQ.deque());
		System.out.println("Count is : " + swabhavQ.count());

		for (String value : swabhavQ) {
			System.out.println(value);
		}
	}

}

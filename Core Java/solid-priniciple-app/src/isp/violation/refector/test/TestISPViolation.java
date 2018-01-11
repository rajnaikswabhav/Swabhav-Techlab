package isp.violation.refector.test;

import isp.violation.refector.IEatable;
import isp.violation.refector.IWorkable;
import isp.violation.refector.Manager;
import isp.violation.refector.Robot;

public class TestISPViolation {

	public static void main(String[] args) {

		Manager manager = new Manager();
		Robot robot = new Robot();

		atTheWorkStation(manager);
		atTheWorkStation(robot);

		atTheCafeteria(manager);
	}

	public static void atTheCafeteria(IEatable eatable) {
		System.out.println("At The Cafeteria....");
		eatable.startEat();
		eatable.stopEat();
		System.out.println();
	}

	public static void atTheWorkStation(IWorkable workable) {
		System.out.println("At The WorkStation....");
		workable.startWork();
		workable.stopWork();
		System.out.println();
	}

}

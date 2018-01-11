package isp.violation.test;

import isp.violation.IWorker;
import isp.violation.Manager;
import isp.violation.Robot;

public class TestISPViolation {

	public static void main(String[] args) {

		Manager manager = new Manager();
		Robot robot = new Robot();
		
		atTheWorkStation(manager);
		atTheWorkStation(robot);
		
		atTheCafeteria(manager);
		atTheCafeteria(robot);
	}
	
	public static void atTheCafeteria(IWorker worker)
	{
		System.out.println("At The Cafeteria....");
		worker.startEat();
		worker.stopEat();
		System.out.println();
	}
	
	public static void atTheWorkStation(IWorker worker)
	{
		System.out.println("At The WorkStation....");
		worker.startWork();
		worker.stopWork();
		System.out.println();
	}

}

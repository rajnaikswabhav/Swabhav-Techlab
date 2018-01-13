package creational.simplefactory;

public class BMW implements IAutomobile {

	@Override
	public void start() {
		System.out.println("BMW car start....");

	}

	@Override
	public void stop() {
		System.out.println("BMW car stop.....");
	}

}

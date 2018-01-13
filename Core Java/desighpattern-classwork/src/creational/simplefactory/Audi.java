package creational.simplefactory;

public class Audi implements IAutomobile {

	@Override
	public void start() {
		System.out.println("Audi car start....");
	}

	@Override
	public void stop() {
		System.out.println("Audi car stop.....");
	}

}

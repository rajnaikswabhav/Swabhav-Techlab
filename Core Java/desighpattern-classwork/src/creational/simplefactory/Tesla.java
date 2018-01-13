package creational.simplefactory;

public class Tesla implements IAutomobile {

	@Override
	public void start() {
		System.out.println("Tesla car start....");
	}

	@Override
	public void stop() {
		System.out.println("Tesla car stop.....");
	}

}

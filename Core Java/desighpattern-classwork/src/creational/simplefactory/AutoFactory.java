package creational.simplefactory;

public class AutoFactory {

	private static AutoFactory autoFactory;

	private AutoFactory() {
	}

	public static AutoFactory getInstance() {
		if (autoFactory == null) {
			autoFactory = new AutoFactory();
		}

		return autoFactory;
	}

	public IAutomobile make(CarType carType) {
		if (carType.equals(CarType.BMW)) {
			return new BMW();
		} else if (carType.equals(CarType.AUDI)) {
			return new Audi();
		} else if (carType.equals(CarType.TESLA)) {
			return new Tesla();
		}

		return null;
	}
}

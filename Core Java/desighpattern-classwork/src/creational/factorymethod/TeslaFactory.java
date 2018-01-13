package creational.factorymethod;

public class TeslaFactory implements IAutoFactory{

	@Override
	public IAutomobile make() {
		return new Tesla();
	}

}

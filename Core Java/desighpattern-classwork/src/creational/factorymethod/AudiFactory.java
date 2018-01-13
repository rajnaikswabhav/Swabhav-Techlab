package creational.factorymethod;

public class AudiFactory implements IAutoFactory {

	@Override
	public IAutomobile make() {
		return new Audi();
	}

}

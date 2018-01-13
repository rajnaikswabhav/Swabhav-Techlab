package creational.factorymethod;

public class BMWFactory implements IAutoFactory{

	@Override
	public IAutomobile  make() {
		return new BMW();

	}

	
}

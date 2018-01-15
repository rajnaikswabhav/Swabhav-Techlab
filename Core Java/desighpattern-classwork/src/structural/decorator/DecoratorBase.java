package structural.decorator;

public abstract class DecoratorBase {

	protected IAccount iAccount;

	public DecoratorBase(IAccount iAccount) {
		this.iAccount = iAccount;
	}
}

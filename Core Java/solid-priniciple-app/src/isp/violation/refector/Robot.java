package isp.violation.refector;

public class Robot implements IWorkable {

	@Override
	public void startWork() {
		System.out.println("Robot Start Working...");
	}

	@Override
	public void stopWork() {
		System.out.println("Robot Stop Working...");
	}

}

package dip.violation.refector;

public class TXTLog implements ILogType {

	@Override
	public void log(String errorMessage)
	{
		System.out.println("Error is writing in txt file: "+errorMessage);
	}

}

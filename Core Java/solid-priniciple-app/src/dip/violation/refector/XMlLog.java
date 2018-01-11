package dip.violation.refector;

public class XMlLog implements ILogType {

	@Override
	public void log(String errorMessage)
	{
		System.out.println("Error is writing in xml file: "+errorMessage);
	}
}

package dip.violation.refector;


public class TaxCalculator {

	private ILogType logger;
	
	public TaxCalculator(ILogType type) {

		this.logger=type;
	}
	public int calculation(int amount , int rate)
	{
		int result=0;
		try{
			result = amount / rate;
		}
		catch(Exception exe)
		{
			logger.log(exe.toString());
		}
		return result;
	}
}

package dip.violation;

public class TaxCalculate {

	private LogType type;
	private XMlLog xmllog;
	private TXTLog txtLog;

	public TaxCalculate(LogType type) {
		this.type=type;
	}
	public int calculation(int amount , int rate)
	{
		int result=0;
		try{
			result = amount / rate;
		}
		catch(Exception e)
		{
			if(type == LogType.XML)
			{
				xmllog = new XMlLog();
				xmllog.log(e.toString());
			}
			else{
				txtLog = new TXTLog();
				txtLog.log(e.toString());
			}
		}
		return result;
	}
}

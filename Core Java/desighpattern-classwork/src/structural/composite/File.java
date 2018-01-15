package structural.composite;

public class File implements IDiskItem {

	private String fileName;
	private double fileSize;
	private String fileExtension;
	private static int count=0;

	public File(String fileName, double fileSize, String fileExtension) {
		this.fileName = fileName;
		this.fileSize = fileSize;
		this.fileExtension = fileExtension;
	}

	@Override
	public void showDetails() {
		
		if(count>0){
			System.out.println("\t\t"+fileName);
		}
		else{
			System.out.println("\t"+fileName);
			count++;
		}
	}

}

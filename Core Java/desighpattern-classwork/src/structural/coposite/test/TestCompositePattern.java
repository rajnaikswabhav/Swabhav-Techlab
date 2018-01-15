package structural.coposite.test;

import structural.composite.File;
import structural.composite.Folder;

public class TestCompositePattern {

	public static void main(String[] args) {

		Folder folder1 = new Folder("Main Folder");
		File file1 = new File("firstFile", 1.55, ".txt");
		folder1.addItem(file1);

		Folder folder2 = new Folder("Sub Folder");
		File file2 = new File("Sub File 1", 1, ".html");
		File file3 = new File("Sub File 2", 2.89, ".csv");
		folder2.addItem(file2);
		folder2.addItem(file3);
		folder1.addItem(folder2);
		
		Folder folder3 = new Folder("Sub Folder 2");
		File file4 = new File("Sub File 3",3,".xml");
		Folder folder4 = new Folder("Sub Folder3");
		folder3.addItem(file4);
		folder3.addItem(folder4);
		folder1.addItem(folder3);
		//System.out.println(""+folder1.getFolderName());
		folder1.showDetails();
	}

}

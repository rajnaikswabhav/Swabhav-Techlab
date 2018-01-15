package structural.composite;

import java.util.ArrayList;
import java.util.List;

public class Folder implements IDiskItem {

	private String folderName;
	private List<IDiskItem> items = new ArrayList<IDiskItem>();
	private static int count = 0;
	public Folder(String folderName) {
		this.folderName = folderName;
	}

	public void addItem(IDiskItem item) {
		items.add(item);
	}

	@Override
	public void showDetails() {

		if(count > 0)
		{
			System.out.println("\t"+folderName);
		}
		else{
			System.out.println(folderName);
			count++;
		}
		for (IDiskItem item : items) {
			item.showDetails();
		}
	}

	public String getFolderName() {
		return folderName;
	}

}

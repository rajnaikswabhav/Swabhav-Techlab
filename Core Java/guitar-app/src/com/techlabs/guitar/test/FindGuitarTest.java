package com.techlabs.guitar.test;

import java.util.Iterator;
import java.util.List;

import com.techlabs.enums.Builder;
import com.techlabs.enums.Type;
import com.techlabs.enums.Wood;
import com.techlabs.guitar.Guitar;
import com.techlabs.guitar.GuitarSpec;
import com.techlabs.guitar.Inventory;

public class FindGuitarTest {
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Inventory inventory = new Inventory();
	    initializeInventory(inventory);

	    GuitarSpec whatErinLikes = 
	    	      new GuitarSpec(Builder.FENDER, "Stratocastor", 
	    	                     Type.ELECTRIC, Wood.ALDER, Wood.ALDER, 6);
	    List<Guitar> matchingGuitars = inventory.search(whatErinLikes);
	    if (!matchingGuitars.isEmpty()) {
	      System.out.println("Erin, you might like these guitars:");
	      for (Iterator<Guitar> i = matchingGuitars.iterator(); i.hasNext(); ) {
	        Guitar guitar = (Guitar)i.next();
	        GuitarSpec spec = guitar.getSpec();
	        System.out.println("  We have a " +
	          spec.getBuilder() + " " + spec.getModel() + " " +
	          spec.getType() + " guitar:\n     " +
	          spec.getBackWood() + " back and sides,\n     " +
	          spec.getTopWood() + " top.\n  You can have it for only $" +
	          guitar.getPrice() + "!\n  ----");
	      }
	    } else {
	      System.out.println("Sorry, Erin, we have nothing for you.");
	    }
	}
	
	private static void initializeInventory(Inventory inventory) {
	    inventory.addGuitar("11277", 3999.95, 
	      new GuitarSpec(Builder.COLLINGS, "CJ", Type.ACOUSTIC,
	                     Wood.INDIAN_ROSEWOOD, Wood.SITKA,6));
	    inventory.addGuitar("V95693", 1499.95, 
	      new GuitarSpec(Builder.FENDER, "Stratocastor", Type.ELECTRIC,
	                     Wood.ALDER, Wood.ALDER,6));
	    inventory.addGuitar("V9512", 1549.95, 
	      new GuitarSpec(Builder.FENDER, "Stratocastor", Type.ELECTRIC,
	                     Wood.ALDER, Wood.ALDER,6));
	    inventory.addGuitar("122784", 5495.95, 
	      new GuitarSpec(Builder.MARTIN, "D-18", Type.ACOUSTIC,
	                     Wood.MAHOGANY, Wood.ADRIONDACK,6));
	    inventory.addGuitar("76531", 6295.95, 
	      new GuitarSpec(Builder.MARTIN, "OM-28", Type.ACOUSTIC,
	                     Wood.BRAZILIAN_ROSEWOOD, Wood.ADRIONDACK,6));
	    inventory.addGuitar("70108276", 2295.95, 
	      new GuitarSpec(Builder.GIBSON, "Les Paul", Type.ELECTRIC,
	                     Wood.MAHOGANY, Wood.MAHOGANY,6));
	    inventory.addGuitar("82765501", 1890.95, 
	      new GuitarSpec(Builder.GIBSON, "SG '61 Reissue", Type.ELECTRIC,
	                     Wood.MAHOGANY, Wood.MAHOGANY,6));
	    inventory.addGuitar("77023", 6275.95, 
	      new GuitarSpec(Builder.MARTIN, "D-28", Type.ACOUSTIC,
	                     Wood.BRAZILIAN_ROSEWOOD, Wood.ADRIONDACK,6));
	    inventory.addGuitar("1092", 12995.95, 
	      new GuitarSpec(Builder.OLSON, "SJ", Type.ACOUSTIC,
	                     Wood.INDIAN_ROSEWOOD, Wood.CEDAR,12));
	    inventory.addGuitar("566-62", 8999.95, 
	      new GuitarSpec(Builder.RYAN, "Cathedral", Type.ACOUSTIC,
	                     Wood.COCOBOLD, Wood.CEDAR,12));
	    inventory.addGuitar("6 29584", 2100.95, 
	      new GuitarSpec(Builder.PRS, "Dave Navarro Signature", Type.ELECTRIC, 
	                     Wood.MAHOGANY, Wood.MAPLE,6));
	  }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var inventory = new Inventory();
            InitializeInventory(inventory);

            GuitarSpec whatErinLikes = new GuitarSpec(Builder.FENDER, "Stratocastor", Type.ELECTRIC,
                                        Wood.ALDER, Wood.ALDER, 6);

            List<Guitar> mathcingGuitars = inventory.Search(whatErinLikes);
            
            if (!mathcingGuitars.Equals(null))
            {
                Console.WriteLine("Erin,You might like this guitars..");

                foreach (var guitar in mathcingGuitars)
                {
         
                    GuitarSpec spec = guitar.Spec;
                    Console.WriteLine("  We have a " +
                                        spec.Builder + " " + spec.Model + " " +
                                        spec.Type + " guitar:\n     " +
                                        spec.BackWood + " back and sides,\n     " +
                                        spec.TopWood + " top.\n  You can have it for only $" +
                                        guitar.Price + "!\n  ----");
                }
            }else
            {
                Console.WriteLine("Sorry, Erin,we have nothing for you....");
            }
        }

        private static void InitializeInventory(Inventory inventory)
        {
            inventory.AddGuitar("11277", 3999.95,
              new GuitarSpec(Builder.COLLINGS, "CJ", Type.ACOUSTIC,
                             Wood.INDIAN_ROSEWOOD, Wood.SITKA, 6));
            inventory.AddGuitar("V95693", 1499.95,
              new GuitarSpec(Builder.FENDER, "Stratocastor", Type.ELECTRIC,
                             Wood.ALDER, Wood.ALDER, 6));
            inventory.AddGuitar("V9512", 1549.95,
              new GuitarSpec(Builder.FENDER, "Stratocastor", Type.ELECTRIC,
                             Wood.ALDER, Wood.ALDER, 6));
            inventory.AddGuitar("122784", 5495.95,
              new GuitarSpec(Builder.MARTIN, "D-18", Type.ACOUSTIC,
                             Wood.MAHOGANY, Wood.ADRIONDACK, 6));
            inventory.AddGuitar("76531", 6295.95,
              new GuitarSpec(Builder.MARTIN, "OM-28", Type.ACOUSTIC,
                             Wood.BRAZILIAN_ROSEWOOD, Wood.ADRIONDACK, 6));
            inventory.AddGuitar("70108276", 2295.95,
              new GuitarSpec(Builder.GIBSON, "Les Paul", Type.ELECTRIC,
                             Wood.MAHOGANY, Wood.MAHOGANY, 6));
            inventory.AddGuitar("82765501", 1890.95,
              new GuitarSpec(Builder.GIBSON, "SG '61 Reissue", Type.ELECTRIC,
                             Wood.MAHOGANY, Wood.MAHOGANY, 6));
            inventory.AddGuitar("77023", 6275.95,
              new GuitarSpec(Builder.MARTIN, "D-28", Type.ACOUSTIC,
                             Wood.BRAZILIAN_ROSEWOOD, Wood.ADRIONDACK, 6));
            inventory.AddGuitar("1092", 12995.95,
              new GuitarSpec(Builder.OLSON, "SJ", Type.ACOUSTIC,
                             Wood.INDIAN_ROSEWOOD, Wood.CEDAR, 12));
            inventory.AddGuitar("566-62", 8999.95,
              new GuitarSpec(Builder.RYAN, "Cathedral", Type.ACOUSTIC,
                             Wood.COCOBOLD, Wood.CEDAR, 12));
            inventory.AddGuitar("6 29584", 2100.95,
              new GuitarSpec(Builder.PRS, "Dave Navarro Signature", Type.ELECTRIC,
                             Wood.MAHOGANY, Wood.MAPLE, 6));
        }

    }
}

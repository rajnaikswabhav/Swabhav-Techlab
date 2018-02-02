using System;

namespace GuitarApp
{
    public enum Builder
    {
        FENDER, MARTIN, GIBSON, COLLINGS, OLSON, RYAN, PRS, ANY
    }
    public enum Type { ACOUSTIC, ELECTRIC }
    public enum Wood
    {
        INDIAN_ROSEWOOD, BRAZILIAN_ROSEWOOD, MAHOGANY, MAPLE,
        COCOBOLD, CEDAR, ADRIONDACK, ALDER, SITKA
    }

    public class GuitarSpec
    {
        private Builder builder;
        private String model;
        private Type type;
        private Wood backWood;
        private Wood topWood;
        private int numStrings;

        public GuitarSpec(Builder builder, String model, Type type, Wood backWood,
            Wood topWood, int numStrings)
        {
            this.builder = builder;
            this.model = model;
            this.type = type;
            this.backWood = backWood;
            this.topWood = topWood;
            this.numStrings = numStrings;

        }

        public bool Matches(GuitarSpec otherSpec)
        {
            if (builder != otherSpec.builder) { return false; }
            if ((model != null) && (!model.Equals(""))
                && (!model.ToLower().Equals(otherSpec.model.ToLower()))) { return false; }
            if (type != otherSpec.type) { return false; }
            if (numStrings != otherSpec.numStrings) { return false; }
            if (backWood != otherSpec.backWood) { return false; }
            if (topWood != otherSpec.topWood) { return false; }

            return true;
        }

        public Builder Builder { get { return builder; } }
        public String Model { get { return model; } }
        public Type Type { get { return type; } }
        public Wood BackWood { get { return backWood; } }
        public Wood TopWood { get { return topWood; } }
        public int NumString { get { return numStrings; } }
    }
}

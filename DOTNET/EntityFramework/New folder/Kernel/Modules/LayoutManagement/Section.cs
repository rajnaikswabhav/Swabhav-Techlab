using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Modules.LayoutManagement
{
    [Table("SECTION")]
    public class Section : DimensionEntity
    {
        public string Name { get; set; }

        public virtual SectionType SectionType { get; set; }
        public virtual ICollection<Access> Accesses { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<Stall> Stalls { get; set; }
        public virtual LayoutPlan LayoutPlan { get; set; }
        public virtual Exhibition Exhibition { get; set; }

        public Section()
        {
            Accesses = new List<Access>();
            Sections = new List<Section>();
            Stalls = new List<Stall>();
        }

        public Section(string name, int height,int width,int x_Coordinate,int y_Coordinate) : this()
        {
            Validate(name, height, width, x_Coordinate, y_Coordinate);

            this.Name = name;
            this.Height = height;
            this.Width = width;
            this.X_Coordinate = x_Coordinate;
            this.Y_Coordinate = y_Coordinate;
        }

        private static void Validate(string name, int height, int width, int x_Coordinate, int y_Coordinate)
        {
            if (string.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Name");
        }

        public static Section Create(string name, int height, int width, int x_Coordinate, int y_Coordinate)
        {
            return new Section(name, height, width, x_Coordinate, y_Coordinate);
        }

        public void Update(string name, int height, int width, int x_Coordinate, int y_Coordinate)
        {
            Validate(name, height, width, x_Coordinate, y_Coordinate);

            this.Name = name;
            this.Height = height;
            this.Width = width;
            this.X_Coordinate = x_Coordinate;
            this.Y_Coordinate = y_Coordinate;
        }
    }
}

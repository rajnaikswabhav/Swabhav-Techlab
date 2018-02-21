using Modules.EventManagement;
using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Modules.SecurityManagement;

namespace Modules.LayoutManagement
{
    [Table("STALL")]
    public class Stall : DimensionEntity
    {
        public int StallNo { get; set; }
        public double Price { get; set; }
        public bool IsBooked { get; set; }
        public int? StallSize { get; set; }
        public bool IsRequested { get; set; }

        public virtual Section Section { get; set; }
        public virtual Pavilion Pavilion { get; set; }
        public virtual Exhibitor Exhibitor { get; set; }
        public virtual Event Event { get; set; }
        //public virtual Login Login { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual ExhibitorIndustryType ExhibitorIndustryType { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }

        public Stall()
        {

        }

        public Stall(int stallNo, double price, bool isBooked, int height, int width, int x_Coordinate, int y_Coordinate, int? stallSize, bool isRequested)
        {
            Validate(stallNo, price, isBooked, height, width, x_Coordinate, y_Coordinate);
            this.StallNo = stallNo;
            this.Price = price;
            this.IsBooked = isBooked;
            this.Height = height;
            this.Width = width;
            this.X_Coordinate = x_Coordinate;
            this.Y_Coordinate = y_Coordinate;
            this.StallSize = stallSize;
            this.IsRequested = isRequested;
        }

        private static void Validate(int stallNo, double price, bool isBooked, int height, int width, int x_Coordinate, int y_Coordinate)
        {

        }

        public static Stall Create(int stallNo, double price, bool isBooked, int height, int width, int x_Coordinate, int y_Coordinate, int? stallSize, bool isRequested)
        {
            return new Stall(stallNo, price, isBooked, height, width, x_Coordinate, y_Coordinate, stallSize, isRequested);
        }

        public void Update(int stallNo, double price, bool isBooked, int height, int width, int x_Coordinate, int y_Coordinate, int? stallSize, bool isRequested)
        {
            Validate(stallNo, price, isBooked, height, width, x_Coordinate, y_Coordinate);
            this.StallNo = stallNo;
            this.Price = price;
            this.IsBooked = isBooked;
            this.Height = height;
            this.Width = width;
            this.X_Coordinate = x_Coordinate;
            this.Y_Coordinate = y_Coordinate;
            this.StallSize = stallSize;
            this.IsRequested = isRequested;
        }
    }
}

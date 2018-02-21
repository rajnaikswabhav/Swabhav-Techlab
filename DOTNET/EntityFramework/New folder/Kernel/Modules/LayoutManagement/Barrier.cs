using Modules.LayoutManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Modules.LayoutManagement
{
    [Table("BARRIER")]
    public class Barrier : MasterEntity
    {
        public int Order { get; set; }
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }

        public virtual Section Section { get; set; }
        public Barrier()
        {
        }

        public Barrier(int order, int x_coordinate, int y_coordinate) : this()
        {
            Validate(order, x_coordinate, y_coordinate);
            this.Order = order;
            this.X_Coordinate = x_coordinate;
            this.Y_Coordinate = y_coordinate;
        }

        private static void Validate(int order, int x_coordinate, int y_coordinate)
        {
        }

        public static Barrier Create(int order, int x_coordinate, int y_coordinate)
        {
            return new Barrier(order, x_coordinate, y_coordinate);
        }

        public void Update(int order, int x_coordinate, int y_coordinate)
        {
            Validate(order, x_coordinate, y_coordinate);
            this.Order = order;
            this.X_Coordinate = x_coordinate;
            this.Y_Coordinate = y_coordinate;
        }
    }
}

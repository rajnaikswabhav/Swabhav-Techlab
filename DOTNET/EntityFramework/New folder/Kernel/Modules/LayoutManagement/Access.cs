using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Modules.LayoutManagement
{
    [Table("ACCESS")]
    public class Access : DimensionEntity
    {
        public bool IsEntry { get; set; }
        public bool IsExit { get; set; }
        public bool IsEmergencyExit { get; set; }

        public virtual Section Section { get; set; }

        public Access()
        {
        }

        public Access(bool isEntry, bool isExit, bool isEmergencyExit, int height, int width, int x_Coordinate, int y_Coordinate) : this()
        {
            Validate(isEntry, isExit, isEmergencyExit, height, width, x_Coordinate, y_Coordinate);

            this.IsEntry = isEntry;
            this.IsExit = isExit;
            this.IsEmergencyExit = isEmergencyExit;
            this.Height = height;
            this.Width = width;
            this.X_Coordinate = x_Coordinate;
            this.Y_Coordinate = y_Coordinate;
        }

        private static void Validate(bool isEntry, bool isExit, bool isEmergencyExit, int height, int width, int x_Coordinate, int y_Coordinate)
        {
            
        }

        public static Access Create(bool isEntry, bool isExit, bool isEmergencyExit, int height, int width, int x_Coordinate, int y_Coordinate)
        {
            return new Access(isEntry, isExit, isEmergencyExit, height, width, x_Coordinate, y_Coordinate);
        }

        public void Update(bool isEntry, bool isExit, bool isEmergencyExit, int height, int width, int x_Coordinate, int y_Coordinate)
        {
            Validate(isEntry, isExit, isEmergencyExit, height, width, x_Coordinate, y_Coordinate);

            this.IsEntry = isEntry;
            this.IsExit = isExit;
            this.IsEmergencyExit = isEmergencyExit;
            this.Height = height;
            this.Width = width;
            this.X_Coordinate = x_Coordinate;
            this.Y_Coordinate = y_Coordinate;
        }
    }
}

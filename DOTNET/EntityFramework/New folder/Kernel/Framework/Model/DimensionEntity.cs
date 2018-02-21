using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Framework.Model
{
    public class DimensionEntity: MasterEntity
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }
    }
}

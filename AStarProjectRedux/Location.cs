using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarProjectRedux
{
    public class Location
    {
        public int X, Y, G, H, F;
        public Location parent;
    }
}

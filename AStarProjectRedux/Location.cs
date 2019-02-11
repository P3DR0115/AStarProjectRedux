using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarProjectRedux
{
    public class Location
    {
        public int X, Y;
        public double G, H, F;
        public Location parent;

        public void CallP()
        {
            // Recursively call the parent and add the location to the Agent's done list. (The list that holds all the efficient coordinates)
            Agent.doneList.Add(this);
            if(parent!= null)
            {
                parent.CallP();

            }
            else
            {
                // If the current location doesn't have a parent, have the Agent reverse the order of the done list
                // And call the method that displays the donelist coordinates.
                Agent.CallBack();
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarProjectRedux
{
    public class Agent
    {
        Location current = null; // Current Location
        Location start = new Location { X = 1, Y = 1 }; //Where the agent is starting
        Location target = new Location { X = 10, Y = 10 }; //Where the agent is heading
        List<Location> openList = new List<Location>(); // Possible places to move
        List<Location> closedList = new List<Location>(); //Places the agent has moved to 
        public static List<Location> doneList = new List<Location>(); // the most effiecent movement list 
        int g = 0; //g score

        public Agent()
        {
            openList.Add(start); 
        }
        
        private static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y); //Computing the H Cost of each location
        } 

        //Writing the most efficient path to the console
        public static void CallBack()
        {
            doneList.Reverse();
            foreach (Location l in doneList)
            {
                Console.WriteLine("[" + l.X + ", " + l.Y + "]"); 
            }
        }

        public void CallParent()
        {
            // Keep calling parent if it isn't the start of null (shouldn't be null)
            doneList.Add(current);
            if (current.parent != null || current.parent != start)
            {
                current.parent.CallP();
            }
        }

        //The actual loop of the agent to move through the map
        public void Play()
        {
            while (openList.Count > 0)
            {
                ChangeCurrent(); //Call the method to change the agent's current location 

                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                {
                    CallParent();
                    break;
                }

                var adjacentSquares = MovementCheck(current.X, current.Y, Game.map);
                g++;

                CalculateFCostofAdjacentSquares(adjacentSquares); //Call the method to calculate F Cost 

                //Game.DiplayMap(); 
            }
        }

        private void CalculateFCostofAdjacentSquares(List<Location> adjacentSquares)
        {
            foreach (var adjacentSquare in adjacentSquares)
            {
                //Checking if the proposed move location is not an available place to move by being blocked
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) != null)
                    continue;

                //Making the choice of where to move
                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) == null) //Checking that the location is open and a movable location
                {
                    if ((adjacentSquare.X + current.X != 0 && adjacentSquare.Y + current.Y != 0) || (adjacentSquare.X - current.X != 0 && adjacentSquare.Y - current.Y != 0)) //Seeing if the current location is the goal, if not, continue to move
                    {
                        adjacentSquare.G =  Math.Sqrt((g * g) + (g * g)); //calculating the g cost 
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y); //calculating the h cost
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H; //calculating the f cost
                        adjacentSquare.parent = current; //setting the newly moved square to the current square
                        openList.Insert(0, adjacentSquare); 
                    }
                }
                //else
                //{
                //    if (adjacentSquare.G + adjacentSquare.H < adjacentSquare.F)
                //    {
                //        adjacentSquare.G = g;
                //        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                //        adjacentSquare.parent = current;
                //    }
                //} We elected to remove this because it was unnecessary and we don't remember why we put it in originally
            }
        }

        //change the current position of the agent
        private void ChangeCurrent()
        {
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);
            closedList.Add(current);
            openList.Remove(current);
        }

        //Create list of all possible places that the agent can move 
        private List<Location> MovementCheck(int x, int y, string[] map)
        {
            List<Location> proposedLocations = new List<Location>()
            {
                // Cardinal Locations
                new Location {X = x, Y = y-1},
                new Location {X = x, Y = y+1},
                new Location {X = x-1, Y = y},
                new Location {X = x+1, Y = y},

                // Diagonals
                new Location {X = x-1, Y = y-1},
                new Location {X = x+1, Y = y+1},
                new Location {X = x-1, Y = y+1},
                new Location {X = x+1, Y = y-1}
            };

           
            return proposedLocations.Where(l => map[l.Y][l.X] == ' ' || map[l.Y][l.X] == 'B').ToList();

        }
        
    }
}

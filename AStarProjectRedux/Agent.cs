using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarProjectRedux
{
    public class Agent
    {
        Location current = null;
        Location start = new Location { X = 1, Y = 1 };
        Location target = new Location { X = 10, Y = 10 };
        List<Location> openList = new List<Location>();
        List<Location> closedList = new List<Location>();
        int g = 0;

        public Agent()
        {
            openList.Add(start);

        }
        
        private static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }

        public void Play()
        {
            while (openList.Count > 0)
            {
                ChangeCurrent();
                DisplayLocation();

                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                    break;//Console.WriteLine("THIS HAD TO BREAK B*TCH");//

                var adjacentSquares = MovementCheck(current.X, current.Y, Game.map);
                g++;

                CalculateFCostofAdjacentSquares(adjacentSquares);

                //Game.DiplayMap();
            }
        }

        private void CalculateFCostofAdjacentSquares(List<Location> adjacentSquares)
        {
            foreach (var adjacentSquare in adjacentSquares)
            {
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) != null)
                    continue;

                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) == null)
                {
                    //if(adjacentSquare.X-current.X != 0 && adjacentSquare.Y-current.Y !=0)
                    //{

                    //}
                    //else
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.parent = current;
                        openList.Insert(0, adjacentSquare);
                    }
                    
                }
                else
                {
                    if (g + adjacentSquare.H < adjacentSquare.F)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.parent = current;
                    }
                }
            }
        }

        private void ChangeCurrent()
        {
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);
            closedList.Add(current);
            openList.Remove(current);
        }

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
                //new Location {X = x-1, Y = y-1},
                //new Location {X = x+1, Y = y+1},
                //new Location {X = x-1, Y = y+1},
                //new Location {X = x+1, Y = y-1}
            };

            return proposedLocations.Where(l => map[l.Y][l.X] == ' ' || map[l.Y][l.X] == 'B').ToList();

        }

        private void DisplayLocation()
        {
            Console.SetCursorPosition(current.X, current.Y);
            Console.Write('.');
            Console.SetCursorPosition(current.X, current.Y);
            System.Threading.Thread.Sleep(1000);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarProjectRedux
{
    class Game
    {
        public static string[] map = new string[]
        {
            //The Map that the Agent is trying to navigate
           //0123456789AB
            "+----------+",//0
            "|A    x    |",//1
            "|   x x  x |",//2
            "| x x x  x |",//3
            "|   x    x |",//4
            "|        x |",//5
            "|  x   x   |",//6
            "|          |",//7
            "|  x x x x |",//8
            "|    x   x |",//9
            "|    x   xB|",//10
            "+----------+" //11
        };

        //Writing the map onto the console
        public static void DiplayMap()
        {
            foreach(string str in map)
            {
                Console.WriteLine(str);
            }
        }
    }
}

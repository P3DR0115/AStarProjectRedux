using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarProjectRedux
{
    class Program
    {
        static void Main()
        {
            //Game g = new Game();
            Agent SnakeEater = new Agent();

            Game.DiplayMap();
            SnakeEater.Play();

            Console.ReadLine();

        }
    }
}

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
            Agent smith = new Agent();

            Game.DiplayMap();
            smith.Play();

            Console.ReadLine();

        }
    }
}

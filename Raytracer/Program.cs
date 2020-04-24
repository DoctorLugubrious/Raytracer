using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    class Program {
        static void Main(string[] args) {
            Scene scene = new Scene();
            Raytracer raytracer = new Raytracer(scene);
            raytracer.OutputFile("..\\..\\result.ppm", 1920, 1080);
            Console.ReadLine();
        }
    }
}

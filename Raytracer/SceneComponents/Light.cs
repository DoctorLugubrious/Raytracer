using Raytracer.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.SceneComponents {
    class Light {
        private readonly Vector3 pos;
        private readonly Color color;

        public Light(Vector3 pos, Color lightColor) {
            this.pos = pos;
            this.color = lightColor;
        }

        public Color Color { get => color; }
        public Vector3 Pos { get => pos; }
    }
}

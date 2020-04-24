using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.SceneComponents {
    class Ray {
        public Ray(Vector3 position, Vector3 direction) {
            Position = position;
            Direction = direction.Normalize();
        }

        public Vector3 Position { get; }
        public Vector3 Direction { get; }

        public float GetDistanceTo(Vector3 point) {
            return Position.GetDistanceTo(point);
        }

        public override string ToString() {
            return "ray(" + Position + " -> " + Direction + ")";
        }

        public Vector3 EvaluateParametric(float var) {
            return Position + var * Direction;
        }

    }
}

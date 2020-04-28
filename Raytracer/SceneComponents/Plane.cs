using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raytracer.Rendering;

namespace Raytracer.SceneComponents {
    //represents a plane in 3D space
    class Plane : SceneObject {
        private readonly Color color;
        private readonly Vector3 pos;
        private readonly Vector3 normal;

        public Plane(Color color, Vector3 pos, Vector3 normal, float reflectivity, float phongConstant): base(reflectivity, phongConstant) {
            this.color = color;
            this.pos = pos;
            this.normal = normal;
            if (normal.IsZeroVector()) {
                throw new ZeroNormalException("Plane was given a zero normal vector");
            }
        }
        public override Color DiffuseColor => color;

        public class ZeroNormalException : Exception {
            public ZeroNormalException(string message): base(message) {  }
        }


        public override Vector3 GetIntersectionPoint(Ray ray) {
            float dotProduct = normal * ray.Direction;
            if (Math.Abs(dotProduct) < 0.001) {
                return null;
            }

            float parametricVariable = (pos - ray.Position) * normal / dotProduct;
            if (parametricVariable <= 0) {
                return null;
            }
            return ray.Position + ray.Direction * parametricVariable;
        }

        public override Vector3 GetNormalAtPoint(Vector3 point) {
            return normal;
        }
    }
}

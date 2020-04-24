using Raytracer.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.SceneComponents {
    class Sphere : SceneObject {
        private readonly Vector3 center;
        private readonly float radius;

        public Sphere(Color diffuseColor, Vector3 center, float radius, float reflectivity, float phongConstant): base(reflectivity, phongConstant) {
            this.DiffuseColor = diffuseColor;
            this.center = center;
            this.radius = radius;
        }

        public override Color DiffuseColor { get; }

        public override Vector3 GetIntersectionPoint(Ray ray) {
            Vector3 rayToCenter = center - ray.Position;
            float closestToCenter = rayToCenter * ray.Direction;
            if (closestToCenter <= 0) {
                return null;
            }

            float distanceToCenter = rayToCenter.Length;
            float fromClosestToHit = (float)Math.Sqrt(
                radius * radius 
                - distanceToCenter * distanceToCenter 
                + closestToCenter * closestToCenter);

            if (fromClosestToHit <= 0) {
                return null;
            }
           

            Vector3 intersectionPoint = ray.EvaluateParametric(closestToCenter - fromClosestToHit);

            return intersectionPoint;
        }

        public override Vector3 GetNormalAtPoint(Vector3 point) {
            return (point - center).Normalize();
        }
    }
}

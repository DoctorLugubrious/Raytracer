using Raytracer.SceneComponents;
using System;

namespace Raytracer.Rendering {
    //tests a scene for intersections.
    class IntersectionTester {
        private readonly Scene scene;

        public struct Intersection {
            public Vector3 intersectionPoint;
            public Vector3 intersectionNormal;
            public SceneObject hitObject;
        }

        public IntersectionTester(Scene scene) {
            this.scene = scene;
        }

        public bool GetInShadow(Ray ray, float lightDistance) {
            foreach (SceneObject current in scene.objects) {
                Vector3 interectionPoint = current.GetIntersectionPoint(ray);
                if (interectionPoint != null && (interectionPoint - ray.Position).Length < lightDistance) {
                    //Console.WriteLine("intersection {0} with object {1}", ray, current);
                    return true;
                }
            }
            return false;
        }
        public Intersection GetClosestObject(Ray ray) {
            float currentMin = float.MaxValue;
            Intersection result = new Intersection(); 

            foreach (SceneObject current in scene.objects) {
                Vector3 interectionPoint = current.GetIntersectionPoint(ray);
                if (interectionPoint != null) {
                    float distance = ray.GetDistanceTo(interectionPoint);
                    if (distance < currentMin) {
                        currentMin = distance;
                        result.intersectionPoint = interectionPoint;
                        result.hitObject = current;
                        result.intersectionNormal = current.GetNormalAtPoint(interectionPoint);
                    }
                }
            }

            return result;
        }
    }
}

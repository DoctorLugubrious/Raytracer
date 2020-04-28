using Raytracer.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.SceneComponents {
    //represents a triangle in 3D space
    class Polygon : SceneObject {
        public Polygon(Color color, Vector3 point1, Vector3 point2, Vector3 point3, float reflectivity, float phongConstant): base(reflectivity, phongConstant) {
            Vector3 side1 = point2 - point1;
            Vector3 side2 = point3 - point2;
            Vector3 normal = side1.Cross(side2).Normalize();
            plane = new Plane(color, point1, normal, reflectivity, phongConstant);

            this.color = color;
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            SetProjectedPoints();
        }

        private void SetProjectedPoints() {
            projectionAxis = GetProjectionAxis();
            point1Projected = GetProjectedPoint(point1);
            point2Projected = GetProjectedPoint(point2);
            point3Projected = GetProjectedPoint(point3);
        }

        private enum ProjectionAxis { X, Y, Z };

        private Vector3 GetProjectedPoint(Vector3 point) {
            switch(projectionAxis) {
                case ProjectionAxis.X:
                    return new Vector3(0, point.Y, point.Z);
                case ProjectionAxis.Y:
                    return new Vector3(point.X, 0, point.Z);
                default:
                    return new Vector3(point.X, point.Y, 0);
            }
        }

        private ProjectionAxis GetProjectionAxis() {
            Vector3 normal = plane.GetNormalAtPoint(new Vector3(0, 0, 0));
            float x = Math.Abs(normal.X);
            float y = Math.Abs(normal.Y);
            float z = Math.Abs(normal.Z);

            if (x > y && x > z) {
                return ProjectionAxis.X;
            }
            else if (y > z) {
                return ProjectionAxis.Y;
            }
            return ProjectionAxis.Z;
        }

        private bool OutwardCrossProduct(Vector3 vector1, Vector3 vector2) {
            Vector3 crossProduct = vector1.Cross(vector2);
            switch (projectionAxis) {
                case ProjectionAxis.X:
                    return crossProduct.X > 0;
                case ProjectionAxis.Y:
                    return crossProduct.Y > 0;
                default:
                    return crossProduct.Z > 0;
            }
        }

        private Plane plane;
        private readonly Color color;
        private readonly Vector3 point1;
        private readonly Vector3 point2;
        private readonly Vector3 point3;

        private ProjectionAxis projectionAxis;

        private Vector3 point1Projected;
        private Vector3 point2Projected;
        private Vector3 point3Projected;

        public override Color DiffuseColor => color;

        public override Vector3 GetIntersectionPoint(Ray ray) {
            Vector3 intersectionPoint = plane.GetIntersectionPoint(ray);
            if (intersectionPoint == null) {
                return null;
            }


            Vector3 pointProjected = GetProjectedPoint(intersectionPoint);

            bool leftSide1 = OutwardCrossProduct(pointProjected - point1Projected, point2Projected - point1Projected);
            bool leftSide2 = OutwardCrossProduct(pointProjected - point2Projected, point3Projected - point2Projected);
            bool leftSide3 = OutwardCrossProduct(pointProjected - point3Projected, point1Projected - point3Projected);
            
            
            if (leftSide1 != leftSide2 || leftSide2 != leftSide3) {
                return null;
            }

            return intersectionPoint;
        }

        public override Vector3 GetNormalAtPoint(Vector3 point) {
            return plane.GetNormalAtPoint(point);
        }
    }
}

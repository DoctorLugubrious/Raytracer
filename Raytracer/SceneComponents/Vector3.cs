using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.SceneComponents {
    //represents a vector in 3D. Supports basic vector operations.
    class Vector3 {
        public Vector3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public float GetDistanceTo(Vector3 other) {
            return (this - other).Length;
        }

        public float Length { get => (float)Math.Sqrt(X * X + Y * Y + Z * Z); }

        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static float operator *(Vector3 a, Vector3 b) => (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);

        public static Vector3 operator *(Vector3 a, float b) => new Vector3(a.X * b , a.Y * b, a.Z * b);
        public static Vector3 operator *(float a, Vector3 b) => b * a;

        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public bool IsZeroVector() {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z) <= 0.001;
        }

        public Vector3 Cross(Vector3 other) {
            return new Vector3(
                Y * other.Z - Z * other.Y,
                Z * other.X - X * other.Z,
                X * other.Y - Y * other.X
            );
        }


        public Vector3 Normalize() {
            float length = Length;
            return new Vector3(X / length, Y / length, Z / length);
        }

        public override string ToString() {
            return "Vector3<" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ">";
        }

    }
}

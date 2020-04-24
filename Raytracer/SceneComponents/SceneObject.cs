using Raytracer.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.SceneComponents {
    abstract class SceneObject {
        public abstract Vector3 GetIntersectionPoint(Ray ray);
        public abstract Color DiffuseColor { get; }
        public abstract Vector3 GetNormalAtPoint(Vector3 point);

        public SceneObject(float reflectivity, float phongConstant) {
            this._reflectivity = new Reflectivity(reflectivity);
            this.phongConstant = phongConstant;
        }

        public float reflectivity { get => _reflectivity.value; }
        public float phongConstant { get; private set; }
        private Reflectivity _reflectivity;
    }
}

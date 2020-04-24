using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Rendering {
    class Reflectivity {
        public float value { get; private set; }
        public Reflectivity(float value) {
            if (value < 0) {
                this.value = 0;
            }
            else if (value > 1) {
                this.value = 1;
            }
            else {
                this.value = value;
            }
        }
    }
}

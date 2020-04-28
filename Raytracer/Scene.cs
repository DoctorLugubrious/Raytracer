using Raytracer.Rendering;
using Raytracer.SceneComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    //represents the scene that will be rendered.
    class Scene {
        public List<SceneObject> objects { get; private set; }

        public Light light { get; private set; }

        public Color ambientLight { get; private set; }

        public float cameraDistance { get; private set; }

        public float fieldOfView { get; private set; }

        public Scene() {
            /* SCENE 1 (assigned in class)
            objects = new List<SceneObject> {
                new Sphere(new Color(255, 255, 255), new Vector3(0.35f, 0, -0.1f), 0.05f, 0.0f, 4),
                new Sphere(new Color(255, 0, 0), new Vector3(0.5f, 0, -0.1f), 0.075f, 0.0f, 32),
                new Sphere(new Color(0, 255, 0), new Vector3(-0.6f, 0, 0f), 0.3f, 0, 32),
                new Polygon(new Color(0, 0, 255), new Vector3(0.3f, -0.3f, -0.4f), new Vector3(0, 0.3f, -0.1f), new Vector3(-0.3f, -0.3f, 0.5f), 0.0f, 32),
                new Polygon(new Color(255, 255, 0), new Vector3(-0.5f, 0.1f, 0.1f), new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-0.5f, 0.1f, -0.3f), 0.0f, 4),
            };

            ambientLight = new Color(25, 25, 25);


            light = new Light(new Vector3(4, 0, 0), new Color(255, 255, 255));

            fieldOfView = ((float)Math.PI / 180) * 28;
            cameraDistance = -1;
            */
            /* SCENE 2 (assigned in class)
            objects = new List<SceneObject> {
                new Sphere(new Color(255, 255, 255), new Vector3(0f, 0.3f, 0), 0.5f, 0.75f, 1),
                new Polygon(new Color(0, 0, 255), new Vector3(0, -0.5f, 0.5f), new Vector3(1, 0.5f, 0f), new Vector3(0f, -0.5f, -0.5f), 0.0f, 4),
                new Polygon(new Color(255, 255, 0), new Vector3(0, -0.5f, 0.5f), new Vector3(0, -0.5f, -0.5f), new Vector3(-1, 0.5f, 0), 0.0f, 4),
            };

            ambientLight = new Color(0, 0, 0);


            light = new Light(new Vector3(0, 10, 0), new Color(255, 255, 255));

            fieldOfView = ((float)Math.PI / 180) * 55;
            cameraDistance = -1.2f;
            */
            //SCENE 3 (my own custom scene)


            objects = new List<SceneObject> {
                new Sphere(new Color(200, 0, 0), new Vector3(1, 0, 4), 0.5f, 0.05f, 20),
                new Sphere(new Color(150, 50, 0), new Vector3(0.7f, 0.7f, 4.25f), 0.5f, 0.05f, 20),
                new Sphere(new Color(100, 100, 0), new Vector3(0, 1, 4.5f), 0.5f, 0.05f, 20),
                new Sphere(new Color(50, 150, 0), new Vector3(-0.7f, 0.7f, 4.75f), 0.5f, 0.05f, 20),
                new Sphere(new Color(0, 200, 0), new Vector3(-0.95f, 0, 5), 0.5f, 0.05f, 20),
                new Sphere(new Color(0, 150, 50), new Vector3(-0.65f, -0.65f, 5.25f), 0.5f, 0.05f, 20),
                new Sphere(new Color(0, 100, 100), new Vector3(0, -0.9f, 5.5f), 0.5f, 0.05f, 20),
                new Sphere(new Color(0, 50, 150), new Vector3(0.6f, -0.6f, 5.75f), 0.5f, 0.05f, 20),

                new Sphere(new Color(0, 0, 200), new Vector3(0.85f, 0, 6), 0.5f, 0.05f, 20),
                new Sphere(new Color(50, 0, 150), new Vector3(0.55f, 0.55f, 6.25f), 0.5f, 0.05f, 20),
                new Sphere(new Color(100, 0, 100), new Vector3(0, 0.8f, 6.5f), 0.5f, 0.05f, 20),
                new Sphere(new Color(150, 0, 50), new Vector3(-0.5f, 0.5f, 6.75f), 0.5f, 0.05f, 20),
                new Sphere(new Color(200, 0, 0), new Vector3(-0.75f, 0, 7), 0.5f, 0.05f, 20),

                new Polygon(new Color(255, 255, 255), new Vector3(-1.45f, 1.1f, 2.25f),  new Vector3(-1.45f, -1.1f, 2.25f), new Vector3(-1.4f, -1.1f, 7), 0.9f, 2),
                new Polygon(new Color(255, 255, 255), new Vector3(-1.45f, 1.1f, 2.25f), new Vector3(-1.4f, -1.1f, 7), new Vector3(-1.4f, 1.1f, 7), 0.9f, 2),

                new Polygon(new Color(255, 255, 255), new Vector3(4.5f, -1.1f, 5f), new Vector3(3, -1.1f, 7), new Vector3(4.5f, 1.1f, 5f),  0.95f, 2),
                new Polygon(new Color(255, 255, 255), new Vector3(3, -1.1f, 7), new Vector3(3, 1.1f, 7), new Vector3(4.5f, 1.1f, 5f), 0.95f, 2),

                new Plane(new Color(255, 255, 255), new Vector3(0, -1.1f, 0), new Vector3(0, 1, 0), 0.17f, 2f),
            };

            ambientLight = new Color(10, 10, 10);


            light = new Light(new Vector3(4, 4, 2), new Color(255, 255, 255));

            fieldOfView = ((float)Math.PI / 180) * 35;
            cameraDistance = 1;
        }
    }
}

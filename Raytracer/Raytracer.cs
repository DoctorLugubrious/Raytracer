using Raytracer.Rendering;
using Raytracer.SceneComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer {
    class Raytracer {
        private readonly IntersectionTester IntersectionTester;
        private Scene scene;
        public Raytracer(Scene scene) {
            this.scene = scene;
            IntersectionTester = new IntersectionTester(scene);

            const float aspectRatio = 16.0f / 9.0f;

            this.viewPlaneHeight = (float)Math.Tan(scene.fieldOfView) * Math.Abs(scene.cameraDistance) * 2;
            this.viewPlaneWidth = aspectRatio * viewPlaneHeight;
        }

        private Color GetObjectColor(IntersectionTester.Intersection point, Vector3 viewFrom) {
            Light light = scene.light;
            Vector3 location = point.intersectionPoint;
            Vector3 normal = point.intersectionNormal;

            Vector3 lightDirection = (light.Pos - location);
            float lightDistance = lightDirection.Length;
            lightDirection = lightDirection.Normalize();

            Vector3 shadowOrigin = location + 0.01f * normal;
            if (IntersectionTester.GetInShadow(new Ray(shadowOrigin, lightDirection), lightDistance)) {
                return scene.ambientLight;
            }

            Vector3 reflect = GetReflectionVector(lightDirection, normal).Normalize();
            float phongConstant = point.hitObject.phongConstant;
            float colorFactor = reflect * viewFrom;
            Color specular =  Math.Sign(colorFactor) * (float)Math.Pow(colorFactor, phongConstant) * scene.light.Color;

            Color diffuse = lightDirection * normal * point.hitObject.DiffuseColor;

            return specular + diffuse + scene.ambientLight;
        }


        private Vector3 GetReflectionVector(Vector3 beam, Vector3 normal) {
            return beam - 2 * (beam * normal) * normal;
        }

        private Color GetColorWithReflections(IntersectionTester.Intersection point, Vector3 direction, int depth) {

            const int MAX_DEPTH = 5;
            if (depth > MAX_DEPTH) {
                return backgroundColor;
            }

            List<KeyValuePair<float, Color>> reflections = new List<KeyValuePair<float, Color>> {
                new KeyValuePair<float, Color>(1 - point.hitObject.reflectivity, GetObjectColor(point, direction))
            };


            const float REFLECTION_FALLOFF = 0.9f;
            float currentWeight = REFLECTION_FALLOFF * point.hitObject.reflectivity;
            IntersectionTester.Intersection currentHit = point;
            Vector3 currentDirection = direction;

            if (currentWeight > 0.01f) {
                Vector3 reflectionVector = GetReflectionVector(currentDirection, currentHit.intersectionNormal).Normalize();
                Vector3 reflectionOrigin = currentHit.intersectionPoint + 0.01f * reflectionVector;
                Ray reflectionRay = new Ray(reflectionOrigin, reflectionVector);
                currentHit = IntersectionTester.GetClosestObject(reflectionRay);

                if (currentHit.hitObject != null) {
                    reflections.Add(new KeyValuePair<float, Color>(currentWeight, GetColorWithReflections(currentHit, reflectionVector, depth + 1)));
                }
                else {
                    reflections.Add(new KeyValuePair<float, Color>(currentWeight, backgroundColor));
                }
            }

            return Color.GetWeightedAverage(reflections);
        }

        private Color GetPixelColor(float x, float y) {
            Vector3 pixelPos = GetWorldSpacePixel(x, y);
            Vector3 pixelDirection = pixelPos - cameraPos;
            Ray pixelRay = new Ray(cameraPos, pixelDirection);

            IntersectionTester.Intersection hit = IntersectionTester.GetClosestObject(pixelRay);
            SceneObject closestObject = hit.hitObject;

            if (closestObject == null) {
                return backgroundColor;
            }
            return GetColorWithReflections(hit, pixelDirection, 0);
        }


        private Vector3 GetWorldSpacePixel(float pixelX, float pixelY) {
            float worldX = viewPlaneWidth * (pixelX / widthInPixels - 0.5f);
            float worldY = viewPlaneHeight * (pixelY / heightInPixels - 0.5f);
            return cameraPos + new Vector3(worldX, worldY, scene.cameraDistance);
        }

        private readonly Vector3 cameraPos = new Vector3(0, 0, 1);
        private readonly float viewPlaneWidth;
        private readonly float viewPlaneHeight;

        private float widthInPixels = 0;
        private float heightInPixels = 0;

        private static readonly Color backgroundColor = new Color(50, 50, 50);

        public void OutputFile(string filename, int width, int height) {
            heightInPixels = height;
            widthInPixels = width;

            using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(filename)) {
                outputFile.WriteLine("P3");
                outputFile.WriteLine(width.ToString() + " " + height.ToString());
                outputFile.WriteLine(Color.colorMax.ToString());
                const int SAMPLE_SLICES = 3;

                for (int y = height - 1; y >= 0; --y) {
                    for (int x = 0; x < width; ++x) {
                        List<KeyValuePair<float, Color>> samples = new List<KeyValuePair<float, Color>>();
                        for (int dy = 0; dy < SAMPLE_SLICES; ++dy) {
                            for (int dx = 0; dx < SAMPLE_SLICES; ++dx) {
                                float sampleX = (2.0f * dx + 1) / (2 * SAMPLE_SLICES);
                                float sampleY = (2.0f * dy + 1) / (2 * SAMPLE_SLICES);
                                samples.Add(new KeyValuePair<float, Color>(1, 
                                    GetPixelColor(x + sampleX, y + sampleY)));
                            }
                        }
                        outputFile.WriteLine(Color.GetWeightedAverage(samples));
                    }
                }
             }
        }
    }
}

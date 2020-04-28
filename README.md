# Raytracer
A simple raytracer I wrote in C#. It generates a ppm file based on what is in the "Scene" class. I wrote all vector math, collision logic, and color logic from scratch.

The raytracer supports spheres, infinite planes, triangles, a single point light source, reflections, and phong shading.

It is fairly slow to run on large images since it does not use parallelization (either with threads or the GPU). Other optimization techniques such as pooling and collision optimization could also speed up the raytracer.


# C-Raytracer
A raytracer created for advanced computer graphics. Includes diffuse + specular lighting, shadows, and can render 
geometry loaded from OBJ files. Objects can have procedurally generated marble textures applied to them using Perlin 
noise.

<p align="center">
<img src="img/grey_sphere.png" height="200" width="200"><img src="img/teapot.png" height="200" width="200"><img src="img/sphere_shadows.png" height="200" width="200"><img src="img/yellow_sphere.png" height="200" width="200">
</p>

Run ray_caster.py to render all the objects within a scene. Runtime has been improved using
bounding spheres and [Numba](https://numba.pydata.org/), however rendering complex meshes can still take some time. Reducing
the size of the camera canvas will make rendering faster, but result in a lower resultion image. 

<p align="center">
  <img src="img/bunny_scene.png" height="400" width="400">
</p>

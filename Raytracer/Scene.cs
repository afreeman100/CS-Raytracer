using System;
using System.Collections.Generic;

namespace Raytracer
{
    public class Scene
    {
        private List<ISceneObject> objects;

        public Scene()
        {
            this.objects = new List<ISceneObject>();
        }
    }
}

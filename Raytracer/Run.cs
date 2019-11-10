namespace Raytracer
{
    public class Run
    {
        public static void Main(string[] args)
        {
            Camera camera = new Camera("img");
            int resolution = 500;

            Scene scene = SceneBuilder.SceneThree();
            //Scene scene = SceneBuilder.SphereGenerator(50);

            camera.Render(resolution, scene);
        }
    }
}

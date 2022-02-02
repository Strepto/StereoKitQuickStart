using System;
using StereoKit;

namespace StereoKitQuickStart_NetCore
{
    public class App
    {
        private Random _random = new Random();
        private Pose _cubePose = new Pose(0, 0, -0.5f, Quat.Identity);
        private Pose _windowPose = new Pose(0.4f, 0, -0.5f, Quat.FromAngles(0, 180, 0));
        private Pose _hamburgerPose = new Pose(-30f, 0, -50f, Quat.Identity);
        public static Material _cubeMaterial = Material.Default.Copy();
        private readonly Model _cube = Model.FromMesh(
            Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
            _cubeMaterial);
        
        
        private Color _color = Color.White;
        private Model _hamburgerModel = null!;

        private readonly Matrix _floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
        private Material _floorMaterial = null!;

        public void Init()
        {
            // Load stuff used by the app
            _floorMaterial = new Material(Shader.FromFile("floor.hlsl"));
            _floorMaterial.Transparency = Transparency.Blend;

            _hamburgerModel = Model.FromFile("Hamburger.glb");
        }



        /// <summary>
        /// Step is invoked every frame (Like the update loop in Unity)
        /// </summary>
        public void Step()
        {
            if (SK.System.displayType == Display.Opaque)
                Default.MeshCube.Draw(_floorMaterial, _floorTransform);

            UI.Handle("Cube", ref _cubePose, _cube.Bounds);
            _cube.Draw(_cubePose.ToMatrix(), _color);

            StepWindow();
            
            Hierarchy.Push(Matrix.S(0.01f));
            UI.Handle("lol", ref _hamburgerPose, _hamburgerModel.Bounds);
            _hamburgerModel.Draw(_hamburgerPose.ToMatrix());
            Hierarchy.Pop();
        }

        // Stuff can be drawn from anywhere in the Step Loop
        private void StepWindow()
        {
            UI.WindowBegin("Window", ref _windowPose);
            
            if (UI.Button("Magic Button"))
            {
                var hue = _random.NextSingle();
                _color = Color.HSV(hue, 0.8f, 0.8f);
            }
            
            UI.WindowEnd();
        }
    }
}
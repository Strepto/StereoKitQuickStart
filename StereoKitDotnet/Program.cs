using System;
using StereoKit;

namespace StereoKitQuickStart_NetCore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Initialize StereoKit
            SKSettings settings = new SKSettings
            {
                appName = "StereoKitQuickStart",
                assetsFolder = "Assets",
            };

            if (!SK.Initialize(settings))
                Environment.Exit(1);

            var app = new App();
            app.Init();

            // Core application loop
            while (SK.Step(() => app.Step()))
            {
                // Intentionally empty
            }

            SK.Shutdown();
        }
    }
}
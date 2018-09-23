using TimeWarp.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class InitializeCameraSystem : IEcsInitSystem
    {
        EcsWorld world = null;

        public void Initialize()
        {
            world.CreateEntityWith<Components.Camera>().transform = UnityEngine.Camera.main.transform;
        }

        public void Destroy()
        {
        }
    }
}

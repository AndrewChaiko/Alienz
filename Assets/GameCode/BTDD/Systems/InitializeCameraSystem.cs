using BTDD.Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace BTDD.Systems
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

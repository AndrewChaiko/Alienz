using TimeWarp.Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class InitializePlayerSystem : IEcsInitSystem
    {
        EcsWorld world = null;

        public void Initialize()
        {
            var playerGo = GameObject.Find("Player");
            var player = world.CreateEntity();
            world.AddComponent<Player>(player).gameObject = playerGo;
            world.GetComponent<Player>(player).transform = playerGo.transform;
            world.AddComponent<Dynamic>(player).rigidbody = playerGo.GetComponent<Rigidbody2D>();
            world.GetComponent<Dynamic>(player).collider = playerGo.GetComponent<Collider2D>();
            var recordable = world.AddComponent<Recordable>(player);
            recordable.positions = new Vector2[300];
            recordable.velocities = new Vector2[300];
            world.AddComponent<Controllable>(player);
        }

        public void Destroy()
        {
        }
    }
}

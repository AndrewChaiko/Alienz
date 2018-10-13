using Leopotam.Ecs;
using TimeWarp.Components;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class PrepareToJumpSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EcsFilter<Player, Dynamic, Controllable> _playerFilter = null;

        public void Run()
        {
            for (int i = 0; i < _playerFilter.EntitiesCount; i++)
            {
                var controllable = _playerFilter.Components3[i];
                var dir = controllable.direction.normalized;

                if (controllable.grounded)
                {
                    Debug.Log(Vector2.Dot(dir, controllable.groundNormal));
                    if (Mathf.Abs(Vector2.Dot(dir, controllable.groundNormal)) <= 0.3f)
                    {
                        controllable.readyToJump = false;
                    }
                    else
                    {
                        controllable.readyToJump = true;
                    }
                }
            }
        }
    }
}

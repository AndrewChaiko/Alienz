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

                if (controllable.grounded)
                {
                    if (Vector2.Dot(controllable.direction, controllable.groundNormal) > 0.5f)
                    {
                        controllable.readyToJump = true;
                    }
                    else
                    {
                        controllable.readyToJump = false;
                    }
                }
            }
        }
    }
}

using TimeWarp.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class PlayerDieSystem : IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter<PlayerDie> _interactionFilter = null;
        private EcsFilter<Player, Dynamic, Controllable>.Exclude<Replay, FrameSelection> _playerFilter = null;

        public void Run()
        {
            for (int i = 0; i < _interactionFilter.EntitiesCount; i++)
            {
                for (int j = 0; j < _playerFilter.EntitiesCount; j++)
                {
                    _playerFilter.Components1[j].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }

                _world.RemoveEntity(_interactionFilter.Entities[i]);
            }
        }
    }
}

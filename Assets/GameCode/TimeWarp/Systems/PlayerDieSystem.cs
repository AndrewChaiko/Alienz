using TimeWarp.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class PlayerDieSystem : IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter<PlayerDie> interactionFilter = null;
        private EcsFilter<Player, Dynamic, Controllable>.Exclude<Replay, FrameSelection> playerFilter = null;

        public void Run()
        {
            for (int i = 0; i < interactionFilter.EntitiesCount; i++)
            {
                for (int j = 0; j < playerFilter.EntitiesCount; j++)
                {
                    playerFilter.Components1[j].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }

                world.RemoveEntity(interactionFilter.Entities[i]);
            }
        }
    }
}

using TimeWarp.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class ReplaySystem : IEcsRunSystem
    {
        EcsWorld world = null;
        EcsFilter<Recordable, Dynamic, Replay, Player> replayablesFilter = null;

        public void Run()
        {
            for (int i = 0; i < replayablesFilter.EntitiesCount; i++)
            {
                int startFrame = replayablesFilter.Components3[i].startFrame;
                int currentFrame = replayablesFilter.Components3[i].currentFrame;
                int selectedFrame = Time.frameCount - currentFrame + startFrame;
                if (selectedFrame < 300)
                {
                    replayablesFilter.Components2[i].rigidbody.MovePosition(replayablesFilter.Components1[i].positions[selectedFrame]);
                }
                else
                {
                    Object.Destroy(replayablesFilter.Components4[i].gameObject);
                    world.RemoveEntity(replayablesFilter.Entities[i]);
                }
            }
        }
    }
}

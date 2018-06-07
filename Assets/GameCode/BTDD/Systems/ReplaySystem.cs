using BTDD.Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace BTDD.Systems
{
    [EcsInject]
    public class ReplaySystem : IEcsRunSystem
    {
        EcsWorld world;
        EcsFilter<Recordable, Dynamic, Replay> replayablesFilter = null;

        public void Run()
        {
            for (int i = 0; i < replayablesFilter.EntitiesCount; i++)
            {
                int startFrame = replayablesFilter.Components3[i].startFrame;
                int currentFrame = replayablesFilter.Components3[i].currentFrame;
                int selectedFrame = Time.frameCount - currentFrame + startFrame;
                if (selectedFrame < 300)
                {
                    //replayablesFilter.Components2[i].rigidbody.velocity = replayablesFilter.Components1[i].velocities[selectedFrame];
                    replayablesFilter.Components2[i].rigidbody.MovePosition(replayablesFilter.Components1[i].positions[selectedFrame]);
                }
                else
                {
                    world.RemoveComponent<Replay>(replayablesFilter.Entities[i]);
                }
            }
        }
    }
}

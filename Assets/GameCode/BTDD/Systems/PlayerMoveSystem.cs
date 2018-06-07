using BTDD.Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace BTDD.Systems
{
    [EcsInject]
    public class PlayerMoveSystem : IEcsRunSystem
    {
        EcsFilter<Dynamic, Player, Controllable>.Exclude<Replay>.Exclude<FrameSelection> playerFilter = null;

        public void Run()
        {
            for (int i = 0; i < playerFilter.EntitiesCount; i++)
            {
                if (Mathf.Abs(playerFilter.Components3[i].horizontal) > Mathf.Epsilon)
                {
                    var velocity = playerFilter.Components1[i].rigidbody.velocity;
                    velocity.x = playerFilter.Components3[i].horizontal * 2;
                    playerFilter.Components1[i].rigidbody.velocity = velocity;
                }
            }
        }
    }
}

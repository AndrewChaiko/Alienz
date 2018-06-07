using BTDD.Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace BTDD.Systems
{
    [EcsInject]
    public class PlayerJumpSystem : IEcsRunSystem
    {
        EcsFilter<Dynamic, Player, Controllable>.Exclude<Replay> playerFilter = null;

        public void Run()
        {
            for (int i = 0; i < playerFilter.EntitiesCount; i++)
            {
                if (playerFilter.Components3[i].jump)
                {
                    playerFilter.Components1[i].rigidbody.AddForce(Vector3.up, ForceMode2D.Impulse);
                }
            }
        }
    }
}

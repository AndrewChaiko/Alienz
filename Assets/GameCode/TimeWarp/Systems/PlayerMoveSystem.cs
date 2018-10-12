using Leopotam.Ecs;
using TimeWarp.Components;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Player, Dynamic, Controllable>.Exclude<Replay, FrameSelection> playerFilter = null;

        private readonly ContactPoint2D[] _contacts = new ContactPoint2D[4];
        private readonly RaycastHit2D[] _results = new RaycastHit2D[4];

        public void Run()
        {
            for (int i = 0; i < playerFilter.EntitiesCount; i++)
            {
                Vector2 input = playerFilter.Components3[i].direction;
                Rigidbody2D body = playerFilter.Components2[i].rigidbody;
                if (playerFilter.Components3[i].grounded)
                {
                    if (Vector2.Dot(_results[0].normal, input) < -0.9f)
                    {
                        body.velocity = Vector2.zero;
                        return;
                    }
                }

                if (Mathf.Abs(playerFilter.Components3[i].direction.x) > Mathf.Epsilon)
                {
                    playerFilter.Components2[i].rigidbody.MovePosition(playerFilter.Components2[i].rigidbody.position + playerFilter.Components3[i].direction * 0.1f);
                }
            }
        }
    }
}

using TimeWarp.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Player, Dynamic, Controllable>.Exclude<Replay, FrameSelection> playerFilter = null;

        private readonly ContactPoint2D[] contacts = new ContactPoint2D[4];
        private readonly RaycastHit2D[] results = new RaycastHit2D[4];
        private readonly int layerMask = ~LayerMask.GetMask("Player");

        public void Run()
        {
            for (int i = 0; i < playerFilter.EntitiesCount; i++)
            {
                Vector2 input = playerFilter.Components3[i].move;
                Rigidbody2D body = playerFilter.Components2[i].rigidbody;
                int contactsCount = Physics2D.CircleCastNonAlloc(playerFilter.Components1[i].transform.position, 0.3f, Vector2.zero, results, 0, layerMask);
                if (contactsCount > 0)
                {
                    if (Vector2.Dot(results[0].normal, input) < -0.9f)
                    {
                        body.velocity = Vector2.zero;
                        return;
                    }
                }

                if (Mathf.Abs(playerFilter.Components3[i].move.x) > Mathf.Epsilon)
                {
                    playerFilter.Components2[i].rigidbody.AddForce(new Vector2(playerFilter.Components3[i].move.x * PhysicsAdjuster.moveForce * body.mass, 0));
                }
            }
        }

        private void SetBodyType(Rigidbody2D body, RigidbodyType2D type)
        {
            if (body.bodyType != type)
            {
                body.bodyType = type;
            }
        }
    }
}

using TimeWarp.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class PlayerJumpSystem : IEcsRunSystem
    {
        private EcsFilter<Player, Dynamic, Controllable>.Exclude<Replay, FrameSelection> playerFilter = null;

        private readonly RaycastHit2D[] results = new RaycastHit2D[4];
        private readonly ContactPoint2D[] contacts = new ContactPoint2D[4];
        private readonly int layerMask = ~LayerMask.GetMask("Player");

        private int jumpboost;
        private Vector2 jumpDirection;

        public void Run()
        {
            for (int i = 0; i < playerFilter.EntitiesCount; i++)
            {
                var collider = playerFilter.Components2[i].collider;
                ContactFilter2D filter = new ContactFilter2D();
                filter.layerMask &= layerMask;
                int contactsCount = Physics2D.CircleCastNonAlloc(collider.transform.position, 0.4f, Vector2.zero, results, 0, layerMask);
                contactsCount = collider.GetContacts(filter, contacts);
                playerFilter.Components3[i].grounded = contactsCount > 0;

                if (playerFilter.Components3[i].jump)
                {
                    if (playerFilter.Components3[i].grounded && jumpboost == 0)
                    {
                        jumpDirection = GetJumpDirection(contacts[0].normal, playerFilter.Components3[i].move);
                    }

                    jumpboost++;
                    float C = Mathf.Cos(Time.deltaTime * jumpboost * PhysicsAdjuster.jumpHeight);
                    if (C > 0)
                    {
                        playerFilter.Components2[i].rigidbody.velocity += C * jumpDirection * PhysicsAdjuster.jumpForce;
                    }
                    else
                    {
                        jumpboost = 0;
                        jumpDirection *= 0;
                        playerFilter.Components3[i].jump = false;
                    }
                }
                else
                {
                    jumpboost = 0;
                    jumpDirection *= 0;
                }
            }
        }

        private Vector2 GetJumpDirection(Vector2 contactNormal, Vector2 input)
        {
            Vector2 returnVector = Vector2.zero;
            var dot = Vector2.Dot(contactNormal, Vector2.up);

            if (Mathf.Abs(dot) < 0.1f)  // on wall
            {
                returnVector = contactNormal + Vector2.up + input;
            }
            else if (dot < 0)                // on ceiling
            {
                returnVector = Vector2.down;
            }
            else                        // on floor
            {
                returnVector = Vector2.up;
            }

            return returnVector.normalized;
        }
    }
}

namespace Card
{
    internal class User
    {
        public string Name => "Andrew";
        public string Surame => "Chaiko";
        public string Email => "andrew.chaiko87@gmail.com";
        public string Telegram => "t.me/shad3rman";
        //public string Twitter => "twitter.com/shad3rman";
        public string Mobile => "+375 (29) 555-72-82";
        //public string Skype => "andrew.chaiko";
        public string LinkedIn => "www.linkedin.com/in/andrewchaiko";
    }
}
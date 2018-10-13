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
                var controllable = playerFilter.Components3[i];
                Rigidbody2D body = playerFilter.Components2[i].rigidbody;
                if (controllable.grounded)
                {
                    if (Vector2.Dot(_results[0].normal, controllable.direction) < -0.9f)
                    {
                        body.velocity = Vector2.zero;
                        return;
                    }
                }

                if (controllable.grounded && !controllable.readyToJump)
                {
                    var tangent = Rotate90(controllable.groundNormal);
                    var resultVector =  tangent * Vector2.Dot(tangent, controllable.direction);
                    Debug.Log(tangent);
                    body.velocity = resultVector * PhysicsAdjuster.moveForce * body.mass;
                }
            }
        }

        public static Vector2 Rotate90(Vector2 normal)
        {
            Vector2 rotated_point;
            rotated_point.x = normal.y;
            rotated_point.y = -normal.x;
            //return Quaternion.Euler(0, 0, c) * normal;
            return rotated_point;
        }
    }
}

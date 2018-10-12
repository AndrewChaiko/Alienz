using TimeWarp.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class PlayerJumpSystem : IEcsRunSystem
    {
        //private readonly EcsFilter<Player, Dynamic, Controllable>.Exclude<Replay, FrameSelection> _playerFilter = null;
        private readonly EcsFilter<Player, Dynamic, Controllable> _playerFilter = null;

        private readonly RaycastHit2D[] results = new RaycastHit2D[4];
        private readonly ContactPoint2D[] contacts = new ContactPoint2D[4];
        private readonly int layerMask = ~LayerMask.GetMask("Player");

        private int _jumpboost;
        private Vector2 _jumpDirection;

        public void Run()
        {
            for (int i = 0; i < _playerFilter.EntitiesCount; i++)
            {
                var controllable = _playerFilter.Components3[i];

                if (controllable.readyToJump && controllable.jump)
                {
                    if (_playerFilter.Components3[i].grounded && _jumpboost == 0)
                    {
                        _jumpDirection = controllable.direction;
                    }

                    _jumpboost++;
                    float C = Mathf.Cos(Time.deltaTime * _jumpboost * PhysicsAdjuster.jumpHeight);
                    if (C > 0)
                    {
                        _playerFilter.Components2[i].rigidbody.velocity += C * _jumpDirection * PhysicsAdjuster.jumpForce;
                    }
                    else
                    {
                        _jumpboost = 0;
                        _jumpDirection *= 0;
                        _playerFilter.Components3[i].jump = false;
                    }
                }
                else
                {
                    _jumpboost = 0;
                    _jumpDirection *= 0;
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

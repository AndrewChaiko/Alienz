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
                var body = _playerFilter.Components2[i].rigidbody;

                if (controllable.readyToJump && controllable.jump && controllable.grounded)
                {
                    body.AddForce(controllable.direction * PhysicsAdjuster.jumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}

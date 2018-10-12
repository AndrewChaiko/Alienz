using Leopotam.Ecs;
using TimeWarp.Components;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class PlayerContactsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Player, Dynamic, Controllable> _playerFilter = null;
        private readonly int _excludePlayerMask = ~LayerMask.GetMask("Player");
        private readonly RaycastHit2D[] _results = new RaycastHit2D[4];
        //private readonly ContactPoint2D[] _contacts = new ContactPoint2D[4];

        public void Run()
        {
            for (int i = 0; i < _playerFilter.EntitiesCount; i++)
            {
                var collider = _playerFilter.Components2[i].collider;
                ContactFilter2D filter = new ContactFilter2D();
                filter.layerMask &= _excludePlayerMask;
                //int contactsCount = collider.GetContacts(filter, _contacts);
                int contactsCount = Physics2D.CircleCastNonAlloc(_playerFilter.Components1[i].transform.position, 0.3f, Vector2.zero, _results, 0, _excludePlayerMask);
                _playerFilter.Components3[i].grounded = contactsCount > 0;
                if (contactsCount > 0)
                {
                    _playerFilter.Components3[i].groundNormal = _results[0].normal;
                    SetBodyType(_playerFilter.Components2[i].rigidbody, RigidbodyType2D.Kinematic); // move to separate system
                }
                else
                {
                    SetBodyType(_playerFilter.Components2[i].rigidbody, RigidbodyType2D.Dynamic);
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

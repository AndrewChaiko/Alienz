using Leopotam.Ecs;
using TimeWarp.Components;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class KeyboardInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Controllable> _controllableFilter = null;

        public void Run()
        {
            for (int i = 0; i < _controllableFilter.EntitiesCount; i++)
            {
                _controllableFilter.Components1[i].direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                _controllableFilter.Components1[i].jump = Input.GetKeyDown(KeyCode.LeftControl);
            }
        }
    }
}

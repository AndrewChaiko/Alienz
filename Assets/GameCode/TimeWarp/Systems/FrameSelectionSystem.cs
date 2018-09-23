using Leopotam.Ecs;
using TimeWarp.Components;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class FrameSelectionSystem : IEcsRunSystem
    {
        EcsFilter<Dynamic, Recordable, FrameSelection> recordablesFilter = null;

        public void Run()
        {
            for (int i = 0; i < recordablesFilter.EntitiesCount; i++)
            {
                int frame = recordablesFilter.Components3[i].frame;
                for (int j = 0; j < recordablesFilter.EntitiesCount; j++)
                {
                    var start = recordablesFilter.Components1[j].rigidbody.position;
                    recordablesFilter.Components1[j].rigidbody.MovePosition(Vector2.Lerp(start, recordablesFilter.Components2[j].positions[frame], Time.deltaTime * 10));
                }
            }
        }
    }
}

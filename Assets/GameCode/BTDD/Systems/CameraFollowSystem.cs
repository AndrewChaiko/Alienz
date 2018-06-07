using BTDD.Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace BTDD.Systems
{
    [EcsInject]
    public class CameraFollowSystem : IEcsRunSystem
    {
        EcsFilter<Controllable, Player> playerFilter = null;
        EcsFilter<Components.Camera> cameraFilter = null;

        public void Run()
        {
            for (int i = 0; i < playerFilter.EntitiesCount; i++)
            {
                for (int j = 0; j < cameraFilter.EntitiesCount; j++)
                {
                    var start = cameraFilter.Components1[j].transform.localPosition;
                    cameraFilter.Components1[j].transform.localPosition = Vector3.Lerp(start, playerFilter.Components2[i].transform.localPosition, Time.deltaTime * 10);
                }
            }
        }
    }
}

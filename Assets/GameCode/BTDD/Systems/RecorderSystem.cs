using BTDD.Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace BTDD.Systems
{
    [EcsInject]
    public class RecorderSystem : IEcsRunSystem
    {
        EcsFilter<Dynamic, Recordable>.Exclude<Replay, FrameSelection> recordablesFilter = null;

        public void Run()
        {
            for (int i = 0; i < recordablesFilter.EntitiesCount; i++)
            {
                if (Time.frameCount >= 300)
                {
                    for (int index = 0; index < 300 - 1; index++)
                    {
                        recordablesFilter.Components2[i].positions[index] = recordablesFilter.Components2[i].positions[index + 1];
                        recordablesFilter.Components2[i].velocities[index] = recordablesFilter.Components2[i].velocities[index + 1];
                    }

                    recordablesFilter.Components2[i].positions[300 - 1] = recordablesFilter.Components1[i].rigidbody.position;
                    recordablesFilter.Components2[i].velocities[300 - 1] = recordablesFilter.Components1[i].rigidbody.velocity;
                }
                else
                {
                    recordablesFilter.Components2[i].positions[Time.frameCount] = recordablesFilter.Components1[i].rigidbody.position;
                    recordablesFilter.Components2[i].velocities[Time.frameCount] = recordablesFilter.Components1[i].rigidbody.velocity;
                }
            }
        }
    }
}

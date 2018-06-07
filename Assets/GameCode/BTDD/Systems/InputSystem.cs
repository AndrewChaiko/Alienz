using BTDD.Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace BTDD.Systems
{
    [EcsInject]
    public class InputSystem : IEcsRunSystem
    {
        EcsFilter<Controllable> controllableFilter = null;
        private Joystick joystick;
        private JumpButton jumpButton;

        public InputSystem(Joystick joystick, JumpButton jumpButton)
        {
            this.joystick = joystick;
            this.jumpButton = jumpButton;
        }

        public void Run()
        {
            for (int i = 0; i < controllableFilter.EntitiesCount; i++)
            {
                controllableFilter.Components1[i].horizontal = joystick.Horizontal;
                controllableFilter.Components1[i].jump = jumpButton.Jump;
            }
        }
    }
}

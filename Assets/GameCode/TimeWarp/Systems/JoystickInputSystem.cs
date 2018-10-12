using TimeWarp.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace TimeWarp.Systems
{
    [EcsInject]
    public class JoystickInputSystem : IEcsRunSystem
    {
        EcsFilter<Controllable> controllableFilter = null;
        private Joystick joystick;
        private JumpButton jumpButton;
        private bool jumpPrevState;

        public JoystickInputSystem(Joystick joystick, JumpButton jumpButton)
        {
            this.joystick = joystick;
            this.jumpButton = jumpButton;
            jumpPrevState = jumpButton.IsDown;
        }

        public void Run()
        {
            for (int i = 0; i < controllableFilter.EntitiesCount; i++)
            {
                controllableFilter.Components1[i].direction = new Vector2(joystick.Horizontal, joystick.Vertical);
                if (jumpButton.IsDown != jumpPrevState)
                {
                    jumpPrevState = jumpButton.IsDown;
                    controllableFilter.Components1[i].jump = jumpButton.IsDown;
                }
            }
        }
    }
}

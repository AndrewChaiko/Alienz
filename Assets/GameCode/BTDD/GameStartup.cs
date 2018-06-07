using BTDD.Systems;
using LeopotamGroup.Ecs;
using LeopotamGroup.Ecs.Ui.Systems;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    EcsWorld _world;

    EcsSystems _systems;

    [SerializeField]
    EcsUiEmitter uiEmitter;
    [SerializeField]
    JumpInTimeSelector jitEmitter;
    [SerializeField]
    Joystick joystick;
    [SerializeField]
    JumpButton jumpButton;

    void OnEnable()
    {
        _world = new EcsWorld();
#if UNITY_EDITOR
        LeopotamGroup.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
#endif  
        _systems = new EcsSystems(_world)
            .Add(jitEmitter)
            .Add(uiEmitter)
            .Add(new InitializePlayerSystem())
            .Add(new InitializeCameraSystem())
            .Add(new InputSystem(joystick, jumpButton))
            .Add(new PlayerMoveSystem())
            .Add(new PlayerJumpSystem())
            .Add(new FrameSelectionSystem())
            .Add(new RecorderSystem())
            .Add(new ReplaySystem())
            .Add(new CameraFollowSystem());
        _systems.Initialize();
#if UNITY_EDITOR
        LeopotamGroup.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
    }

    void Update()
    {
        _systems.Run();
    }

    void OnDisable()
    {
        _systems.Destroy();
    }
}
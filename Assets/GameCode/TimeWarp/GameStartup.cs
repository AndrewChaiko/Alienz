using TimeWarp.Systems;
using LeopotamGroup.Ecs;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    EcsWorld _world;

    EcsSystems _gameplaySystems;
    EcsSystems _physicsSystems;

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
        _gameplaySystems = new EcsSystems(_world)
            //.Add(jitEmitter)
            .Add(new InitializePlayerSystem())
            .Add(new InitializeCameraSystem())
            .Add(new InputSystem(joystick, jumpButton))
            //.Add(new FrameSelectionSystem())
            //.Add(new RecorderSystem())
            //.Add(new ReplaySystem())
            .Add(new CameraFollowSystem())
            .Add(new PlayerDieSystem());
        _gameplaySystems.Initialize();

        _physicsSystems = new EcsSystems(_world)
            .Add(new PlayerMoveSystem())
            .Add(new PlayerJumpSystem());
        _physicsSystems.Initialize();

#if UNITY_EDITOR
        LeopotamGroup.Ecs.UnityIntegration.EcsSystemsObserver.Create(_gameplaySystems);
#endif
    }

    void Update()
    {
        _gameplaySystems.Run();
    }

    private void FixedUpdate()
    {
        _physicsSystems.Run();
    }

    void OnDisable()
    {
        _gameplaySystems.Destroy();
        _physicsSystems.Destroy();
    }
}
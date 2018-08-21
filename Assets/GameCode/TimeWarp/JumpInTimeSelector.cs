using TimeWarp.Components;
using LeopotamGroup.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[EcsInject]
public class JumpInTimeSelector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEcsRunSystem
{
    private EcsWorld world = null;

    private EcsFilter<Dynamic, Recordable>.Exclude<Replay> recordablesFilter = null;
    private EcsFilter<FrameSelection> frameSelectionFilter = null;
    private EcsFilter<Dynamic, Player, Controllable, Recordable> playersFilter = null;
    private Slider slider;
    private bool captured;
    private int releasedFrame;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        captured = true;
        ToggleCapture(captured);

        for (int i = 0; i < recordablesFilter.EntitiesCount; i++)
        {
            world.AddComponent<FrameSelection>(recordablesFilter.Entities[i]).frame = (int)slider.value;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        captured = false;
        ToggleCapture(captured);
        DuplicatePlayer();

        for (int i = 0; i < frameSelectionFilter.EntitiesCount; i++)
        {
            var entity = frameSelectionFilter.Entities[i];
            world.RemoveComponent<FrameSelection>(entity);
            var replay = world.AddComponent<Replay>(entity);
            replay.startFrame = frameSelectionFilter.Components1[i].frame;
            releasedFrame = Time.frameCount - replay.startFrame;
            replay.currentFrame = releasedFrame;
        }
    }

    public void Run()
    {
        if (Time.frameCount - releasedFrame < 300 && !captured)
        {
            slider.value = Time.frameCount - releasedFrame;
        }
    }

    private void OnSliderValueChanged(float value)
    {
        for (int i = 0; i < frameSelectionFilter.EntitiesCount; i++)
        {
            frameSelectionFilter.Components1[i].frame = (int)value;
        }
    }

    private void ToggleCapture(bool iscaptured)
    {
        for (int i = 0; i < recordablesFilter.EntitiesCount; i++)
        {
            recordablesFilter.Components1[i].rigidbody.bodyType = iscaptured ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
        }
    }

    private void DuplicatePlayer()
    {
        for (int i = 0; i < playersFilter.EntitiesCount; i++)
        {
            var newPlayerGo = Instantiate(playersFilter.Components2[i].gameObject);
            newPlayerGo.name = "Player";
            var newPlayer = world.CreateEntity();
            world.AddComponent<Player>(newPlayer).gameObject = newPlayerGo;
            world.GetComponent<Player>(newPlayer).transform = newPlayerGo.transform;
            world.AddComponent<Dynamic>(newPlayer).rigidbody = newPlayerGo.GetComponent<Rigidbody2D>();
            var recordable = world.AddComponent<Recordable>(newPlayer);
            recordable.positions = new Vector2[300];
            recordable.velocities = new Vector2[300];
            world.AddComponent<Controllable>(newPlayer);
            playersFilter.Components2[i].gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            playersFilter.Components2[i].gameObject.name = "Shadow";

            world.RemoveComponent<Controllable>(playersFilter.Entities[i]);
        }
    }
}

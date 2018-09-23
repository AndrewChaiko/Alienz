using Leopotam.Ecs;
using UnityEngine;

public abstract class CollisionEntityEmitter<T> : MonoBehaviour where T : class, new()
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCollision(other.gameObject);
    }

    protected abstract void OnCollision(GameObject other);

    protected T EmitEntity()
    {
        return EcsWorld.Active.CreateEntityWith<T>();
    }
}

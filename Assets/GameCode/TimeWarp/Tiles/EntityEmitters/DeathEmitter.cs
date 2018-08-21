using TimeWarp.Components;
using UnityEngine;

namespace TimeWarp.Tiles.EntityEmitters
{
    public class DeathEmitter : CollisionEntityEmitter<PlayerDie>
    {
        protected override void OnCollision(GameObject other)
        {
            if (other.gameObject.name == "Player")
            {
                EmitEntity();
            }
        }
    }
}

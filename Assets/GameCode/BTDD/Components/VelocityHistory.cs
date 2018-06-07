using UnityEngine;

namespace BTDD.Components
{
    public class VelocityHistory
    {
        public VelocityEntry[] entries;
    }

    public struct VelocityEntry
    {
        public int tick;
        public Vector2 velocity;
    }
}

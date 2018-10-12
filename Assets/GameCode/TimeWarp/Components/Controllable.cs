using UnityEngine;

namespace TimeWarp.Components
{
    public class Controllable
    {
        public bool jump;
        public bool readyToJump;
        public bool grounded;
        public Vector2 groundNormal;
        public Vector2 direction;
    }
}

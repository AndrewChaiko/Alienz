using UnityEngine;

public class PhysicsAdjuster : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbodyPlayer = null;

    public static float moveForce = 3;
    public static float jumpForce = 1;
    public static float jumpHeight = 8;
    private static bool showGui = false;

    private void Awake()
    {
        moveForce = 21;
        jumpForce = 1.5f;
        jumpHeight = 4.5f;
        rigidbodyPlayer.gravityScale = 2.8f;
    }

    private void OnGUI()
    {
        GUI.matrix = Matrix4x4.Scale(new Vector3(Screen.width / 1280f, Screen.height / 720f, 1));

        if (GUI.Button(new Rect(1, 10, 50, 50), "GUI"))
        {
            showGui = !showGui;
        }

        if (showGui)
        {
            GUI.Label(new Rect(1, 100, 100, 50), "DRAG " + rigidbodyPlayer.drag);
            rigidbodyPlayer.drag = GUI.HorizontalSlider(new Rect(100, 100, 1000, 50), rigidbodyPlayer.drag, 0, 10);
            GUI.Label(new Rect(1, 150, 100, 50), "MASS " + rigidbodyPlayer.mass);
            rigidbodyPlayer.mass = GUI.HorizontalSlider(new Rect(100, 150, 1000, 50), rigidbodyPlayer.mass, 0, 5);
            GUI.Label(new Rect(1, 200, 100, 50), "POS ITER " + Physics2D.positionIterations);
            Physics2D.positionIterations = (int)GUI.HorizontalSlider(new Rect(100, 200, 1000, 50), Physics2D.positionIterations, 4, 24);
            GUI.Label(new Rect(1, 250, 100, 50), "VEL ITER " + Physics2D.velocityIterations);
            Physics2D.velocityIterations = (int)GUI.HorizontalSlider(new Rect(100, 250, 1000, 50), Physics2D.velocityIterations, 4, 24);
            GUI.Label(new Rect(1, 300, 100, 50), "MOVE FORCE " + moveForce);
            moveForce = GUI.HorizontalSlider(new Rect(100, 300, 1000, 50), moveForce, 1, 50);
            GUI.Label(new Rect(1, 350, 100, 50), "JUMP FORCE " + jumpForce);
            jumpForce = GUI.HorizontalSlider(new Rect(100, 350, 1000, 50), jumpForce, 1, 5);
            GUI.Label(new Rect(1, 400, 100, 50), "GRAVITY SCALE " + rigidbodyPlayer.gravityScale);
            rigidbodyPlayer.gravityScale = GUI.HorizontalSlider(new Rect(100, 400, 1000, 50), rigidbodyPlayer.gravityScale, 1, 10);
            GUI.Label(new Rect(1, 450, 100, 50), "JUMP HEIGHT " + jumpHeight);
            jumpHeight = GUI.HorizontalSlider(new Rect(100, 450, 1000, 50), jumpHeight, 1, 15);
        }
    }
}

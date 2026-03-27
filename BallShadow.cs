using UnityEngine;

public class BallShadow : MonoBehaviour
{
    public Transform ball; // ball (drag)
    public float yOffset = 0.01f; // on Y

    void Update()
    {
        if (ball != null)
        {
            Vector3 pos = ball.position; // get ball position
            pos.y = yOffset; // with offset
            transform.position = pos; // follow ball position
        }
    }
}
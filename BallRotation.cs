using UnityEngine;

public class BallRotation : MonoBehaviour
{
    public Vector3 speed = new Vector3(20, 20, 0);

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}

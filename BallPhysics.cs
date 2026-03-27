using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    private Rigidbody rb;
    public AudioSource wallBounceAudioSource; // 🎧 reference to the audio obj for the wall bounce

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // We get the Rigidbody component
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (wallBounceAudioSource != null) // 🎧 check if there is a sound (drag)
                wallBounceAudioSource.Play(); // 🎧 and play WIN! sound
            
            // Bounce effect: reflect the velocity based on the collision normal
            Vector3 incomingVelocity = rb.linearVelocity; //vector of previous speed
            Vector3 normal = collision.contacts[0].normal; // perpendicular vector

            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal); // reflect bounce
            rb.linearVelocity = reflectedVelocity * 1.2f; // at the same speed x 1.2
        }
    }
}







using UnityEngine;

// It needs to be attached to a GameObject with a Rigidbody.

public class Player2Controller : MonoBehaviour // 2️⃣ for player 2 
{
    public float weight = 80; // weight of the player;
    public float speed = 45f; // Movement speed
    private Rigidbody rb;
    public float jumpspeed = 1000f; // Jump speed
    private bool jumpRequested = false;
    private bool isJumping = false; // 🚫 jumping check
    public float jumpBoost = 5f; // ⏫ For the jump speed boost
    public float fallBoost = 25f; // ⏬ For the fall speed boost
    // public float maxHeight = 15f; // 📏 Max jump height
    public float ballBounceForce = 100f; // Ball bounce force
    public AudioSource playerBounceAudioSource; // 🎧 reference to the audio obj for the player bounce

    //KEYS:
    public KeyCode jumpKey = KeyCode.W; // 2️⃣ for player 2 
    public string horizontalAxis = "Horizontal2"; // 2️⃣ for player 2 

    private void Start() // Runs on start
    {
        rb = GetComponent<Rigidbody>(); // We get the Rigidbody component
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; // To block the 2D
    }

    // runs on 60fps
    void Update()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping) // Detect input at 60 fps and 🚫 check the jumping so that you can't send request midair 
        {
            jumpRequested = true;
            rb.mass = weight; // set weight of the player;
        }
    }

    // slower - better for movement with physics. (Doesn't check every frame, that's why jump is declared in Update)
    private void FixedUpdate()
    {
        // TO MOVE ↔️ ---------------------------------------
        // gets Horizontal input —  (A/D or Left/   Right)
        // D = +1 (right), A = -1 (left), nothing = 0
        float moveHorizontal = Input.GetAxis(horizontalAxis);

        // create direction to move (left/right) and multiply it ➡️ = ▶️*↔️*💨*⏱
        Vector3 velocity = Vector3.right * moveHorizontal * speed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, 0); // force Z to 0 for the 2D

        // move the player by adding the movement to its position! 🔴+➡️
        //rb.MovePosition(rb.position + movement);


        // TO JUMP ⬆️ --------------------------------------- if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping) 
        if (jumpRequested && !isJumping) //if you press up and you are not jumping 🚫
        {
            //rb.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse); // jump
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpspeed / rb.mass, 0); // New Jump
            isJumping = true; // 🚫 jumping check
            jumpRequested = false; // reset after jump
        }

        // TO FIX JUMP SPEED AND HEIGHT: ⏫
        if (isJumping && rb.linearVelocity.y > 0) // ⏫ if player is moving up
        {
            rb.AddForce(Vector3.up * jumpBoost, ForceMode.Acceleration); // ⏫ add a boost to defy gravity
        }

        if (isJumping && rb.linearVelocity.y < 0) // ⏬ If the player is moving down
        {
            rb.AddForce(Vector3.down * fallBoost, ForceMode.Acceleration); // ⏬ add a boost to push down fast
        }

        //Vector3 pos = rb.position; // 📏pos is the actual player position
        //if (pos.y > maxHeight) // 📏if it's more then the height I have set
        //{
        //    pos.y = maxHeight; // 📏stop it
        //    rb.position = pos; // 📏and stop the player
        //    rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // 📏stop upward velocity you cant move up
        //}
    }

    // TO KNOW IF YOU CAN JUMP -------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) //if he is touching ground
        {
            isJumping = false; // he is jumping = false 🚫 jumping check
        }

        if (collision.gameObject.CompareTag("Ball")) //when touching ball
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>(); //get rigidbody
            if (ballRb != null)
            {
                Vector3 bounceDirection = (collision.transform.position - transform.position).normalized; // calc vector from player to ball
                ballRb.AddForce(bounceDirection * ballBounceForce, ForceMode.Impulse); // push in that direction with decided force
            }
            if (playerBounceAudioSource != null) // 🎧 check if there is a sound (drag)
                playerBounceAudioSource.Play(); // 🎧 and play bounce sound
        }
    }
}
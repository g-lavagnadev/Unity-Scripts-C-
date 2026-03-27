using UnityEngine;

public class BallScoring : MonoBehaviour
{
    public GameManager gameManager; // the script to the obj GameManager (drag) 
    private bool hasScored = false; 

    void OnCollisionEnter(Collision collision)
    {
        if (hasScored) return;

        if (collision.gameObject.name == "Ground") // if Ball hits Ground
        {
            float ballX = transform.position.x; // bring to the position selected in drag option

            if (ballX < 0) // if ball falls in the red side
            {
                gameManager.AddPointToPlayer(1);// P1 scores
            }
            else // else, on green side
            {
                gameManager.AddPointToPlayer(2); // P2 scores
            }

            hasScored = true;
            // 1st bounce doesn't count:
            Invoke(nameof(ResetHasScored), 3f); // add 3 sec delay til next score can be counted
        }
    }

    // reset hasScored to count points after 1st
    public void ResetHasScored()
    {
        hasScored = false;
    }
}

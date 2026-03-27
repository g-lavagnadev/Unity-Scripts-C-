using UnityEngine;
using TMPro; // 🅾️2️⃣ for the HUD

public class GameManager : MonoBehaviour
{

    public int player1Score = 0; // Tracks player scores
    public int player2Score = 0;
    public Transform ballTransform; // Ball position (drag ball)
    public AudioSource pointAudioSource; // 🎧 reference to the audio obj for score
    public AudioSource winAudioSource; // 🎧 reference to the audio obj for WIN!

    public Vector3 player1StartPos = new Vector3(20, 20, -2.57f);   // Player 1's ball reset position
    public Vector3 player2StartPos = new Vector3(-20, 20, -2.57f);  // Player 2's ball reset position

    public TextMeshProUGUI P1ScoreText; // 🅾️2️⃣ for the HUD
    public TextMeshProUGUI P2ScoreText; // 🅾️2️⃣ for the HUD
    public TextMeshProUGUI winText; // 🆎💯 for the win HUD
    public float winDisplayTime = 2f; // 🆎💯 time win message shows
    public int winScore = 10; // 🆎💯 max score

    public void AddPointToPlayer(int playerNumber)// Called when point scored
    {
        // +1 player score for the right player
        if (playerNumber == 1)
            player1Score++;
        else if (playerNumber == 2)
            player2Score++;

        // 🅾️2️⃣ for the HUD : Update the UI 
        if (P1ScoreText != null) // 🅾️2️⃣ if there is a HUD
            P1ScoreText.text = $"P1: {player1Score}"; // 🅾️2️⃣set it to P1 score
        if (P2ScoreText != null) // 🅾️2️⃣
            P2ScoreText.text = $"P2: {player2Score}"; // 🅾️2️⃣ set it to P2 score

        // 🆎💯Check win condition
        if (player1Score >= winScore) // 🆎💯 if score>10
        {
            StartCoroutine(HandleWin(1)); // 🆎💯 P1 wins
            if (winAudioSource != null) // 🎧 check if there is a sound (drag)
                winAudioSource.Play(); // 🎧 and play WIN! sound
        }
        else if (player2Score >= winScore) // 🆎💯
        {
            StartCoroutine(HandleWin(2)); // 🆎💯
            if (winAudioSource != null) // 🎧 check if there is a sound (drag)
                winAudioSource.Play(); // 🎧 and play WIN! sound
        }
        else // if it's a normal point
        {
            ResetBallToPlayerSide(playerNumber == 1 ? 2 : 1); // Reset ball normally
            if (pointAudioSource != null) // 🎧 check if there is a sound (drag)
                pointAudioSource.Play(); // 🎧 and play point-scoring sound
        }
    }

    // Resets the ball to the given player's side of the field
    private void ResetBallToPlayerSide(int playerToServe)
    {
        if (ballTransform == null) return; // Make sure the ball is dragged in Unity

        if (playerToServe == 1)
            ballTransform.position = player1StartPos; // move the ball in start position 1
        else
            ballTransform.position = player2StartPos; // move the ball in start position 2


        Rigidbody rb = ballTransform.GetComponent<Rigidbody>(); // reset rigidbody velocity to stop it
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    
    private System.Collections.IEnumerator HandleWin(int playerNumber) // 🆎💯 for the win HUD
    {
        if (winText != null) // 🆎💯 for the win HUD
        {
            winText.text = $"PLAYER {playerNumber} WINS!"; // 🆎💯 change win text with the winning player
            winText.gameObject.SetActive(true); // 🆎💯 and activate HUD of the win
        }

        yield return new WaitForSeconds(winDisplayTime); // 🆎💯 for the win HUD : wait the secs you decided

        if (winText != null) // 🆎💯 if there is a wintext
            winText.gameObject.SetActive(false); // 🆎💯 hide win text

        player1Score = 0; // 🆎💯 reset score
        player2Score = 0; // 🆎💯 reset score

        if (P1ScoreText != null) // 🆎💯 if there is UI
            P1ScoreText.text = $"P1: 0"; // 🆎💯 reset UI
        if (P2ScoreText != null) // 🆎💯 if there is UI
            P2ScoreText.text = $"P2: 0"; // 🆎💯 reset UI

        ResetBallToPlayerSide(playerNumber == 1 ? 2 : 1); // 🆎💯 Reset ball
    }

}

using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public int scorePerHit = 100; // Set this in the Inspector


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Blackboard.s_Instance.AddScore(scorePerHit); // Just add points
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            ScoreManager.Instance.IncreaseScore(50);
            ScoreManager.Instance.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}

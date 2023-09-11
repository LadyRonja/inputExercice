
using UnityEngine;

public class EnemyTargetPractice : MonoBehaviour
{
    private bool teleportQueued = false;
    public GameObject scoreBallPrefab;

    private void Start()
    {
        Teleport(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Laser>() != null)
        {

            if (!teleportQueued)
            {
                if (Random.Range(1, 101) <= 5)
                {
                    Instantiate(this.gameObject);
                }

                teleportQueued = true;

                Invoke("Teleport", 0.3f);
            }


        }
    }

    private void Teleport()
    {
        Teleport(true);
    }

    private void Teleport(bool spawnScore)
    {
        Camera cam = Camera.main;
        float worldWidth = Camera.main.orthographicSize * 2 * ((float)Screen.width / Screen.height);
        float worldHeight = Camera.main.orthographicSize * 2;

        float randX = Random.Range(cam.transform.position.x - worldWidth / 2, cam.transform.position.x + worldWidth / 2);
        float randY = Random.Range(cam.transform.position.y - worldHeight / 2, cam.transform.position.y + worldHeight / 2);
        Vector3 newPos = new Vector3(randX, randY, 0);

        if (spawnScore) SpawnScore();

        transform.position = newPos;

        teleportQueued = false;
    }

    private void SpawnScore()
    {
        Instantiate(scoreBallPrefab, transform.position, Quaternion.identity);
    }
}

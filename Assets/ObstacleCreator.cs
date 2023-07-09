using System.Collections;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    public GameObject potPrefab;
    public GameObject knifePrefab;
    public Camera mainCamera;
    public Vector2 lastObstaclePosition;
    public GameObject[] spawnPoints;

    private float generationX;

    void Start()
    {
        generationX = mainCamera.scaledPixelWidth / 2f;
        StartCoroutine(ThrowKnives(8f));
    }

    private IEnumerator ThrowKnives(float waitTime)
    {
        while (true)
        {
            var randomY = Random.Range(-1f, 2f);
            yield return new WaitForSeconds(waitTime + randomY);
            var knifeSpawnPoint = spawnPoints[1];
            var p = knifeSpawnPoint.transform.position;
            var spawnPoint = new Vector2(p.x, p.y + randomY);
            Instantiate(knifePrefab, spawnPoint, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        var obstaclePositionOnScreen = mainCamera.WorldToScreenPoint(lastObstaclePosition);

        if (obstaclePositionOnScreen.x <= generationX)
        {
            CreateObstacle();
        }
    }

    private void CreateObstacle()
    {
        var potSpawnPoint = spawnPoints[0];
        var spawnPoint = potSpawnPoint.transform.position;
        lastObstaclePosition = Instantiate(potPrefab, spawnPoint, Quaternion.identity).transform.position;
    }
}

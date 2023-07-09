using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    public GameObject potPrefab;
    public Camera mainCamera;
    public Vector2 lastObstaclePosition;
    public GameObject[] spawnPoints;

    private float generationX;

    void Start()
    {
        generationX = mainCamera.scaledPixelWidth / 2f;
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
        var spawnPoint = spawnPoints[0].transform.position;
        lastObstaclePosition = Instantiate(potPrefab, spawnPoint, Quaternion.identity).transform.position;
    }
}

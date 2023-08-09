using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    public GameObject ObstaclePrefab;
    public GameObject ObstacleParent;
    public int ObstacleCount = 0;

    /*    public int startSeed = 43;
    */
    public int ObstaclesNumber;
    public float ObstacleSpawnInterval = 1f;
    public float ObstacleReturnInterval = 2f;
    public float ObstacleMoveDistance = 0f;

    private GameObject[] ObstacleObjects = new GameObject[10];
    private int currentObstacleIndex = 0;
    private float timer = 0f;

    private void Start()
    {
        Random.InitState(42);
        for (int i = 0; i < ObstaclesNumber; i++)
        {
            GameObject ObstacleObject = ObjectPoolManager.SpawnObjects(ObstaclePrefab, new Vector3(7.757677f, -5.657569f, -9f), Quaternion.identity, ObstacleParent);
            ObstacleObject.SetActive(false);

            ObstacleObjects[i] = ObstacleObject;
        }

        ObstacleObjects[0].SetActive(true);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= ObstacleSpawnInterval)
        {
            timer = 0f;

            GameObject previousObstacle = ObstacleObjects[currentObstacleIndex];

            currentObstacleIndex = (currentObstacleIndex + 1) % ObstaclesNumber;

            GameObject newObstacle = ObstacleObjects[currentObstacleIndex];
            /*            newObstacle.transform.position = previousObstacle.transform.position + new Vector3(ObstacleMoveDistance, ObstacleMoveDistance, 0.1f);
            */
            float angleInRadians = Mathf.Deg2Rad * 30f;
            float moveX = ObstacleMoveDistance * Mathf.Cos(angleInRadians) + Random.Range(-1.2f, 1.2f);
            float moveY = ObstacleMoveDistance * Mathf.Sin(angleInRadians);
            newObstacle.transform.position = previousObstacle.transform.position + new Vector3(moveX, moveY, -0.01f);

            newObstacle.SetActive(true);

            StartCoroutine(DeactivatePreviousObstacle(previousObstacle));
        }
    }

    private IEnumerator DeactivatePreviousObstacle(GameObject ObstacleToDeactivate)
    {
        yield return new WaitForSeconds(ObstacleReturnInterval);

        ObstacleToDeactivate.SetActive(false);
        ObjectPoolManager.ReturnObjectsToPool(ObstacleToDeactivate);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    public GameObject ObstaclePrefab;
    public GameObject ObstacleParent;
    public GameObject Camera;
    public int ObstacleCount = 0;
    public float RandomnessUp = 0f;
    public float RandomnessDown = 0f;
    /*    public int startSeed = 43;
    */
    public int ObstaclesNumber;
    public float ObstacleSpawnInterval = 1f;
    public float ObstacleReturnInterval = 2f;
    public float ObstacleMoveDistance = 0f;

    private GameObject[] ObstacleObjects = new GameObject[10];
    private int currentObstacleIndex = 0;
    private float timer = 0f;
    private float offset;
    private void Start()
    {
        ObstacleObjects = new GameObject[ObstaclesNumber]; 
        Random.InitState(40);
        for (int i = 0; i < ObstaclesNumber; i++)
        {
            GameObject ObstacleObject = ObjectPoolManager.SpawnObjects(ObstaclePrefab, new Vector3(7.757677f, -6.657569f, -9f), Quaternion.identity, ObstacleParent);
            ObstacleObject.SetActive(false);

            ObstacleObjects[i] = ObstacleObject;
        }

/*        ObstacleObjects[0].SetActive(true);
*/    }

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
            float moveX = ObstacleMoveDistance * Mathf.Cos(angleInRadians);
            float moveY = ObstacleMoveDistance * Mathf.Sin(angleInRadians);

            /*            Vector3 offset = new Vector3(moveX * currentObstacleIndex, moveY * currentObstacleIndex, 0f);
            */
            /*            newObstacle.transform.position =  new Vector3(previousObstacle.transform.position.x + moveX, moveY, -0.01f);
            */
            // Add randomness to Y-axis within a range (-RandomnessDown to RandomnessUp)
            float randomYOffset = Random.Range(-RandomnessDown, RandomnessUp);

            // Ensure the Y value doesn't go over a certain limit
            float minYLimit = Mathf.Abs(Camera.transform.position.y) - 25;
            float maxYLimit = Mathf.Abs(Camera.transform.position.y) + 10;

            float newY = previousObstacle.transform.position.y + moveY + randomYOffset;

            // Clamp newY to stay within the specified Y limits
            newY = Mathf.Clamp(newY, minYLimit, maxYLimit);

            // Apply the new position
            newObstacle.transform.position = new Vector3(previousObstacle.transform.position.x + moveX, newY, -0.01f);

            newObstacle.SetActive(true);

            StartCoroutine(DeactivatePreviousObstacle(previousObstacle));
        }
    }
    //* Random.Range(RandomnessDown, RandomnessUp);
    private IEnumerator DeactivatePreviousObstacle(GameObject ObstacleToDeactivate)
    {
        yield return new WaitForSeconds(ObstacleReturnInterval);

        ObstacleToDeactivate.SetActive(false);
        ObjectPoolManager.ReturnObjectsToPool(ObstacleToDeactivate);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapHandler : MonoBehaviour
{
    public GameObject TilemapPrefab;
    public GameObject TilemapParent;
/*    public int startSeed = 43;
*/    
    public int TileMapsNumber = 17;
    public int MaxActiveTilemaps = 15;  // Define the maximum number of active tilemaps
    public float TIleMapSpawnInterval = 1f;
    public float TIleMapReturnInterval = 2f;
    public float TIleMapMoveDistance = 0f;
    private bool isFinished = false;
    private GameObject[] tilemapObjects = new GameObject[10];
    private int currentTilemapIndex = 0;
    private float timer = 0f;

    private void Start()
    {
        tilemapObjects = new GameObject[TileMapsNumber];

        for (int i = 0; i < TileMapsNumber; i++)
        {
            GameObject tilemapObject = ObjectPoolManager.SpawnObjects(TilemapPrefab, new Vector3(5.6f, -28.1f, 0f), Quaternion.identity, TilemapParent);
            tilemapObject.SetActive(false);

            tilemapObjects[i] = tilemapObject;
        }

        tilemapObjects[0].SetActive(true);
        tilemapObjects[1].SetActive(true);
        tilemapObjects[2].SetActive(true);
        tilemapObjects[1].transform.position = new Vector3(35.91089f, -10.6f, 0.01f);
        tilemapObjects[2].transform.position = new Vector3(66.399f, 7.005f, 0.02f); 
       
    }

    private void FixedUpdate()
    {
        isFinished = FindFirstObjectByType<GameSceneManager>().GetComponent<GameSceneManager>().IsFinised;
        if (!isFinished)
        {
            GenerateTilemap();
        }
        else
        {
            StopAllCoroutines();
            tilemapObjects[currentTilemapIndex].SetActive(true);
        }
    }


    public void GenerateTilemap()
    {
        // Check the number of active tilemaps
        int activeTilemaps = CountActiveTilemaps();

        // Don't generate more tilemaps if there are too many active arleady
        if (activeTilemaps >= MaxActiveTilemaps)
            return;  

        timer += Time.deltaTime;

        if (timer >= TIleMapSpawnInterval)
        {
            timer = 0f;

            GameObject previousTilemap = tilemapObjects[currentTilemapIndex];

            currentTilemapIndex = (currentTilemapIndex + 1) % TileMapsNumber;

            GameObject newTilemap = tilemapObjects[currentTilemapIndex];

            float angleInRadians = Mathf.Deg2Rad * 30f;
            float moveX = TIleMapMoveDistance * Mathf.Cos(angleInRadians);
            float moveY = TIleMapMoveDistance * Mathf.Sin(angleInRadians);

            // calculating new position of next tilemap
            newTilemap.transform.position = previousTilemap.transform.position + new Vector3(moveX, moveY, 0.01f);

            newTilemap.SetActive(true);

            StartCoroutine(DeactivatePreviousTilemap(previousTilemap));
        }
    }

    private int CountActiveTilemaps()
    {
        int count = 0;

        foreach (GameObject tilemap in tilemapObjects)
        {
            if (tilemap.activeSelf)
                count++;
        }

        return count;
    }

    // deactivating tilemap after precised amount of time
    private IEnumerator DeactivatePreviousTilemap(GameObject tilemapToDeactivate)
    {
        yield return new WaitForSeconds(TIleMapReturnInterval);

        tilemapToDeactivate.SetActive(false);
        ObjectPoolManager.ReturnObjectsToPool(tilemapToDeactivate);
    }
}
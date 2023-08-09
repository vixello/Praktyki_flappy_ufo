using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapHandler : MonoBehaviour
{
    public GameObject TilemapPrefab;
    public GameObject TilemapParent;
/*    public int startSeed = 43;
*/    
    public int TileMapsNumber = 5;
    public float TIleMapSpawnInterval = 1f;
    public float TIleMapReturnInterval = 2f;
    public float TIleMapMoveDistance = 0f;

    private GameObject[] tilemapObjects = new GameObject[10];
    private int currentTilemapIndex = 0;
    private float timer = 0f;

    /*    private void Start()
        {
            for (int i = 0; i < TileMapsNumber; i++)
            {
                int seed = startSeed + i;
                GameObject tilemapObject = ObjectPoolManager.SpawnObjects(TilemapPrefab, new Vector3(5.6f, -28.1f, 0f), Quaternion.identity, TilemapPrefab.GetComponent<TilemapGenerator>().ParentObject);

                TilemapGenerator tilemapGenerator = tilemapObject.GetComponent<TilemapGenerator>();
                if (tilemapGenerator != null)
                {
                    tilemapGenerator.SetSeed(seed);
                    tilemapGenerator.GenerateTilemap();
                }
                tilemapObject.SetActive(false);

                tilemapObjects[i] = tilemapObject;
            }

            tilemapObjects[currentTilemapIndex].SetActive(true);
        }*/
    private void Start()
    {
        for (int i = 0; i < TileMapsNumber; i++)
        {
            GameObject tilemapObject = ObjectPoolManager.SpawnObjects(TilemapPrefab, new Vector3(5.6f, -28.1f, 0f), Quaternion.identity, TilemapParent);
            tilemapObject.SetActive(false);

            tilemapObjects[i] = tilemapObject;
        }

        tilemapObjects[0].SetActive(true);
        tilemapObjects[1].SetActive(true);
        tilemapObjects[1].transform.position = new Vector3(35.91089f, -10.6f, 0.01f);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= TIleMapSpawnInterval)
        {
            timer = 0f;

            GameObject previousTilemap = tilemapObjects[currentTilemapIndex];

            currentTilemapIndex = (currentTilemapIndex + 1) % TileMapsNumber;

            GameObject newTilemap = tilemapObjects[currentTilemapIndex];
            /*            newTilemap.transform.position = previousTilemap.transform.position + new Vector3(TIleMapMoveDistance, TIleMapMoveDistance, 0.1f);
            */
            float angleInRadians = Mathf.Deg2Rad * 30f;
            float moveX = TIleMapMoveDistance * Mathf.Cos(angleInRadians);
            float moveY = TIleMapMoveDistance * Mathf.Sin(angleInRadians);
            newTilemap.transform.position = previousTilemap.transform.position + new Vector3(moveX, moveY, 0.01f);

            newTilemap.SetActive(true);

            StartCoroutine(DeactivatePreviousTilemap(previousTilemap));
        }
    }

    private IEnumerator DeactivatePreviousTilemap(GameObject tilemapToDeactivate)
    {
        yield return new WaitForSeconds(TIleMapReturnInterval);

        tilemapToDeactivate.SetActive(false);
        ObjectPoolManager.ReturnObjectsToPool(tilemapToDeactivate);
    }
}
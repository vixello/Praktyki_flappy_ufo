using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjects> ObjectPools = new List<PooledObjects>();
    public GameObject tileMap;
    private void Update()
    {
        ObjectPoolManager.SpawnObjects(tileMap, new Vector3(100, 100, 100), Quaternion.identity);
    }
    public static GameObject SpawnObjects(GameObject toSpawnObject, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        PooledObjects pool = ObjectPools.Find(p => p.LookupString == toSpawnObject.name);

        if (pool == null)
        {
            pool = new PooledObjects() { LookupString = toSpawnObject.name };
            ObjectPools.Add(pool);
        }
        // find inactive objects
        GameObject spawnableObject = null;
        foreach (GameObject obj in pool.InactiveObjects)
        {
            if (spawnableObject != null)
            {
                spawnableObject = obj;
                break;
            }
        }

        if (spawnableObject == null)
        {
            spawnableObject = Instantiate(toSpawnObject, spawnPosition, spawnRotation);
        }

        else
        {
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }
        return spawnableObject;
    }
    public static void ReturnObjectsToPool(GameObject returnedObject)
    {
        string name = returnedObject.name.Substring(0, returnedObject.name.Length - 1);

        PooledObjects pool = ObjectPools.Find(p => p.LookupString == name);

        if (pool == null)
        {
            Debug.Log("Releasing an object that is not pooled");
        }
        else
        {
            returnedObject.SetActive(false);
            pool.InactiveObjects.Add(returnedObject);
        }
    }

    public class PooledObjects
    {
        public string LookupString;
        public List<GameObject> InactiveObjects = new List<GameObject>();
    }
}
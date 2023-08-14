
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjects> ObjectPools = new List<PooledObjects>();
    public GameObject PoolObject;
    public enum PoolType
    {
        Gameobject,
        None
    }

    private static GameObject _objectPoolEmptyHandler;
    private static GameObject _poolObjectEmpty;
    private Coroutine returnObjectToPoolCoroutine;
    private void Awake()
    {
/*        SetupEmpty();
*/    }

    private void SetupEmpty()
    {
        _objectPoolEmptyHandler = new GameObject("Pooled objects");

        _poolObjectEmpty = new GameObject("GameObjects");

        _poolObjectEmpty.transform.SetParent(_objectPoolEmptyHandler.transform);
        
    }
    private void OnEnable()
    {
        returnObjectToPoolCoroutine = StartCoroutine(ReturnObjectsToPool());
    }
    public static GameObject SpawnObjects(GameObject toSpawnObject, Vector3 spawnPosition, Quaternion spawnRotation, GameObject parentObject = null)
    {
        PooledObjects pool = ObjectPools.Find(p => p.LookupString == toSpawnObject.name);
        
        if (pool == null)
        {
            pool = new PooledObjects() { LookupString = toSpawnObject.name };
            ObjectPools.Add(pool);
        }

        // find inactive objects
        GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();

        if (spawnableObject == null)
        {
/*            GameObject parentObject = SetParentObject(poolType);
*/            spawnableObject = Instantiate(toSpawnObject, spawnPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObject.transform.SetParent(parentObject.transform);
            }
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
        string name = returnedObject.name.Substring(0, returnedObject.name.Length - 7);

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

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.None:
                return null;
            case PoolType.Gameobject:
                return _poolObjectEmpty;
            default:
                return null;
        }
    }
    public class PooledObjects
    {
        public string LookupString;
        public List<GameObject> InactiveObjects = new List<GameObject>();
    }

    private IEnumerator ReturnObjectsToPool()
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        ReturnObjectsToPool(gameObject);
    }
}

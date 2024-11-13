using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;
    public static ObjectPoolManager Instance => _instance;

    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int poolSize;
    }

    public class GameObjectPools
    {
        public string name;
        public List<GameObject> gameObjects = new List<GameObject>();
        public GameObject prefab;
    }

    [SerializeField] public List<Pool> pools;

    List<GameObjectPools> objectPool;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
        InitPool();
    }

    public void InitPool()
    {
        objectPool = new List<GameObjectPools>();
        foreach (Pool pool in pools)
        {
            GameObjectPools gameObjectList = new GameObjectPools();
            for (int i = 0; i < pool.poolSize; i++)
            {

                GameObject @object = Instantiate(pool.prefab, transform);
                @object.name = pool.prefab.name;
                gameObjectList.name = @object.name;
                gameObjectList.prefab = pool.prefab;
                @object.SetActive(false);
                gameObjectList.gameObjects.Add(@object);
            }
            objectPool.Add(gameObjectList);
        }

    }

    public GameObject GetPoolObject(string name)
    {

        foreach (GameObject gameObject in findGoodPool(name).gameObjects)
        {
            if (!gameObject.activeInHierarchy)
            {
                return gameObject;
            }

        }

        GameObject newObject = Instantiate(findGoodPool(name).prefab, transform);
        newObject.SetActive(false);
        findGoodPool(name).gameObjects.Add(newObject);
        return newObject;

    }

    public GameObjectPools findGoodPool(string name)
    {
        GameObjectPools list = new GameObjectPools();
        foreach (GameObjectPools GOlist in objectPool)
        {
            if (GOlist.name == name)
            {
                list = GOlist;
            }
        }
        return list;
    }
    public void ResetPool()
    {
        foreach (GameObjectPools lists in objectPool)
        {
            foreach (GameObject obj in lists.gameObjects)
            {
                Destroy(obj);
            }
        }
    }
}
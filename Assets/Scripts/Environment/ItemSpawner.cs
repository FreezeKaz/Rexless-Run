using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    class Obstacles
    {
        public List<Obstacle> obstacles;
    }
    [SerializeField] private List<Obstacles> _obstacles;
    [SerializeField] private List<GameObject> _spawnedObjects;
    [SerializeField] private List<Transform> _spawnPoints;

    private bool isSpawning = false;
    private float baseSpawnDelay = 3f;
    private Transform _previousSpawnPoint;
    private Transform _actualSpawnPoint;

    // Update is called once per frame
    public void StartSpawning()
    {
        // Start spawning if the game is playing and we're not already spawning
        if (GameManager.Instance._playing && !isSpawning)
        {
            StartCoroutine(SpawnObstacles());
        }
    }

    IEnumerator SpawnObstacles()
    {
        isSpawning = true;

        while (GameManager.Instance._playing)
        {
            int difficultyCheck = GameManager.Instance.Difficulty > _obstacles.Count ? _obstacles.Count : GameManager.Instance.Difficulty;
            int randomIndex = Random.Range(0, difficultyCheck);
            int randomIndex2 = Random.Range(0, _obstacles.Count);


            GameObject temp = ObjectPoolManager.Instance.GetPoolObject(_obstacles[randomIndex].obstacles[randomIndex2].name);
            temp.SetActive(true);
            Obstacle tempObstacle = temp.GetComponent<Obstacle>();
            tempObstacle.Init();
            tempObstacle.ObjectHasToDisappear += TakeOffObject;
            _spawnedObjects.Add(temp);

            if (GameManager.Instance._speed < 1.8)
            {
                do
                {
                    _actualSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                } while (_actualSpawnPoint == _previousSpawnPoint && _previousSpawnPoint == null);
                _previousSpawnPoint = _actualSpawnPoint;
                temp.transform.position = _actualSpawnPoint.position;
            }
            else
            {
                _actualSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                _previousSpawnPoint = _actualSpawnPoint;
                temp.transform.position = _actualSpawnPoint.position;
            }



            float spawnDelay = baseSpawnDelay / GameManager.Instance._speed;
            yield return new WaitForSeconds(spawnDelay);
        }

        isSpawning = false;
    }

    public void StopSpawning()
    {
        StopCoroutine(SpawnObstacles());
        isSpawning = false;
    }

    public void TakeOffObject(GameObject go)
    {
        _spawnedObjects.Remove(go);
        go.GetComponent<Obstacle>().ObjectHasToDisappear -= TakeOffObject;
    }
    public void DisableObstacles()
    {
        foreach (GameObject obstacle in _spawnedObjects)
        {
            obstacle.SetActive(false);
        }
    }


}

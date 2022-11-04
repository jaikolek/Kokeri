using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPointList;
    [SerializeField] private Transform obstacleContainer;
    [SerializeField] private List<GameObject> groundObstaclePrefabList;
    [SerializeField] private List<GameObject> flyingObstaclePrefabList;
    [SerializeField] private Transform bugSpawnPoint;
    [SerializeField] private Transform bugContainer;
    [SerializeField] private GameObject bugPrefab;

    private void Start()
    {
        HutanEventManager.Instance.OnGameStarted += HutanEventManager_OnGameStarted;
        HutanEventManager.Instance.OnDespawned += HutanEventManager_OnDespawned;
    }

    private void Update()
    {
        if (HutanGameManager.Instance.IsGameReady && !isChanceToSpawnBug)
            StartCoroutine(ChanceToSpawnBug());
    }

    private void HutanEventManager_OnGameStarted()
    {
        SpawnObstacle();
    }


    private void HutanEventManager_OnDespawned()
    {
        SpawnObstacle();
    }

    private bool isChanceToSpawnBug = false;
    private IEnumerator ChanceToSpawnBug()
    {
        isChanceToSpawnBug = true;
        yield return new WaitForSeconds(2f);
        if (Random.Range(0, 100) < 50)
            SpawnBug();
        isChanceToSpawnBug = false;
    }

    private void SpawnObstacle()
    {
        if (Random.Range(0, 100) < 60)
        {
            // random obstacle
            int randomObstacle = Random.Range(0, groundObstaclePrefabList.Count);
            // spawn obstacle
            Instantiate(groundObstaclePrefabList[randomObstacle], spawnPointList[0].position, Quaternion.identity, obstacleContainer);
        }
        else
        {
            // random spawn point
            int randomSpawnPoint = Random.Range(1, spawnPointList.Count);
            // random obstacle
            int randomObstacle = Random.Range(0, flyingObstaclePrefabList.Count);
            // spawn obstacle
            Instantiate(flyingObstaclePrefabList[randomObstacle], spawnPointList[randomSpawnPoint].position, Quaternion.identity, obstacleContainer);
        }
    }

    private void SpawnBug()
    {
        Instantiate(bugPrefab, bugSpawnPoint.position, Quaternion.identity, bugContainer);
    }
}

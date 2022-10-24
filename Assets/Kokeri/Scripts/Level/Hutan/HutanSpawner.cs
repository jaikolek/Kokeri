using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPointList;
    [SerializeField] private Transform obstacleContainer;
    [SerializeField] private List<GameObject> obstaclePrefabList;
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
        if (Random.Range(0, 100) < 30)
            SpawnBug();
        isChanceToSpawnBug = false;
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, spawnPointList.Count);
        int randomObstacle = Random.Range(0, obstaclePrefabList.Count);

        Transform spawnPoint = spawnPointList[randomIndex];
        GameObject obstacle = obstaclePrefabList[randomObstacle];

        Instantiate(obstacle, spawnPoint.position, Quaternion.identity, obstacleContainer);
    }

    private void SpawnBug()
    {
        Instantiate(bugPrefab, bugSpawnPoint.position, Quaternion.identity, bugContainer);
    }
}

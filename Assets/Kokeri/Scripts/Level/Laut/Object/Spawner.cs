using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabObjek;
    [SerializeField]
    private GameObject[] prefabPos;
    public bool isSpawning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    public void StartSpawning()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        int waitTime = Random.Range(3, 4);
        yield return new WaitForSeconds(waitTime);
        int randomPrefab = Random.Range(0, prefabObjek.Length);
        int randomPos = Random.Range(0, prefabPos.Length);
        Instantiate(prefabObjek[randomPrefab], prefabPos[randomPos].transform.position, Quaternion.identity);
        if (isSpawning)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    
}

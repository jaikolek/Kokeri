using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabObjek;
    [SerializeField]
    private GameObject[] prefabObjek2;
    [SerializeField]
    private GameObject[] prefabPos;
    [SerializeField]
    private GameObject[] prefabPos2;


    public bool isSpawning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObject());
    }
    public void StartSpawning()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnObject()
    {
        int waitTime = Random.Range(3, 4);
        yield return new WaitForSeconds(waitTime);
        int randomPrefab = Random.Range(0, prefabObjek.Length);
        int randomPos = Random.Range(0, prefabPos.Length);
        Instantiate(prefabObjek[randomPrefab], prefabPos[randomPos].transform.position, Quaternion.identity);
        if (isSpawning)
        {
            StartCoroutine(SpawnObject());
            StartCoroutine(SpawnIkanBesar());
        }
    }

    IEnumerator SpawnIkanBesar()
    {
        int waitTime = Random.Range(5, 6);
        yield return new WaitForSeconds(waitTime);
        int randomPrefab = Random.Range(0, prefabObjek2.Length);
        int randomPos = Random.Range(0, prefabPos2.Length);
        Instantiate(prefabObjek2[randomPrefab], prefabPos2[randomPos].transform.position, Quaternion.identity);
    }
    
}

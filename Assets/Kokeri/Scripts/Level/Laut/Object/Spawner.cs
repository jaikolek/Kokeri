using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabObjek;
    [SerializeField]
    private GameObject[] prefabPos;
    [SerializeField]
    private GameObject[] gelembungPos;
    [SerializeField]
    private GameObject[] sampah;
    [SerializeField]
    private GameObject gelembung;

    public float waktuSpawnSampah;

    public bool isSpawning = true;

    // Start is called before the first frame update
    void Start()
    {

        if (isSpawning)
        {
            StartCoroutine(SpawnIkan());
            StartCoroutine(SpawnSampah());
            StartCoroutine(SpawnGelembung());
        }
    }
    IEnumerator SpawnIkan()
    {
        int waitTime = Random.Range(4, 5);
        yield return new WaitForSeconds(waitTime);
        int randomPrefab = Random.Range(0, prefabObjek.Length);
        int randomPos = Random.Range(0, prefabPos.Length);
        if(randomPrefab == 0 || randomPrefab == 1)
        {
            randomPos = Random.Range(1, 2);
        }
        Instantiate(prefabObjek[randomPrefab], prefabPos[randomPos].transform.position, Quaternion.identity);
        StartCoroutine(SpawnIkan());

    }

    IEnumerator SpawnSampah()
    {
        yield return new WaitForSeconds(waktuSpawnSampah);
        int randomPrefab = Random.Range(0, sampah.Length);
        int randomPos = Random.Range(0, prefabPos.Length);
        Instantiate(sampah[randomPrefab], prefabPos[randomPos].transform.position, Quaternion.identity);
        StartCoroutine(SpawnSampah());
        
    }

    IEnumerator SpawnGelembung()
    {
        int waitTime = Random.Range(2, 3);
        yield return new WaitForSeconds(waitTime);
        int randomPos = Random.Range(0, gelembungPos.Length);
        GameObject gel = Instantiate(gelembung, gelembungPos[randomPos].transform.position, Quaternion.identity);
        float randomScale = Random.Range(0.4f, 1f);
        gel.transform.localScale = new Vector3(randomScale, randomScale, 0);
        StartCoroutine(SpawnGelembung());
    }

}

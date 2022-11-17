using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] gameobject;
    public Spawner spawner;
    public static float tempSpeed;

    private float timer = 0f;

    private void Start()
    {
        AudioManager.Instance.PlayBGM("Laut");
        Time.timeScale = 1;
        timer = Time.timeSinceLevelLoad;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameobject[0].SetActive(false);
        gameobject[1].SetActive(true);

    }

    private void Update()
    {

        timer += Time.deltaTime;

        if(timer >= 60f && timer < 120f)
        {
            spawner.waktuSpawnSampah = 7f;
            tempSpeed = 2.5f;

        }
        else if (timer >= 120f && timer < 180f)
        {
            spawner.waktuSpawnSampah = 6f;
            tempSpeed = 3f;

        }
        else if(timer >= 180f)
        {
            spawner.waktuSpawnSampah = 5f;
            tempSpeed = 3.5f;
        }
        else
        {
            spawner.waktuSpawnSampah = 9f;
            tempSpeed = 2f;
        }
    }

}
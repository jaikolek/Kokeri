using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Spawner spawner;
    public static float tempSpeed;

    private float timer = 0f;

    private void Start()
    {
        LautProlog.OnVideoEnd += LautProlog_OnVideoEnd;
    }

    private void LautProlog_OnVideoEnd()
    {
        AudioManager.Instance.PlayBGM("Laut");
        timer = Time.timeSinceLevelLoad;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Peta()
    {
        SceneManager.LoadScene(1);
    }


    private void Update()
    {

        timer += Time.deltaTime;

        if (timer >= 60f && timer < 120f)
        {
            spawner.waktuSpawnSampah = 4.5f;
            tempSpeed = 2.5f;

        }
        else if (timer >= 120f && timer < 180f)
        {
            spawner.waktuSpawnSampah = 3.5f;
            tempSpeed = 3f;
        }
        else if (timer >= 180f)
        {
            spawner.waktuSpawnSampah = 2.5f;
            tempSpeed = 3.5f;
        }
        else
        {
            spawner.waktuSpawnSampah = 5f;
            tempSpeed = 2.1f;
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    #region singleton
    private static SceneHandler instance;
    public static SceneHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneHandler>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(SceneHandler).Name;
                    instance = obj.AddComponent<SceneHandler>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion singleton

    public bool isDesaPrologPlayed = false;
    public bool isHutanPrologPlayed = false;
    public bool isLautPrologPlayed = false;

    public event Action<string> OnSceneChanged;
    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
        OnSceneChanged?.Invoke(_sceneName);
    }

    public event Action OnSceneReloaded;
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnSceneReloaded?.Invoke();

        OnSceneChanged?.Invoke(SceneManager.GetActiveScene().name);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HutanGameManager : MonoBehaviour
{
    #region singleton
    private static HutanGameManager instance;
    public static HutanGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HutanGameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.transform.parent = GameObject.Find("ManagerContainer").transform;
                    obj.name = typeof(HutanGameManager).Name;
                    instance = obj.AddComponent<HutanGameManager>();
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
        }
    }
    #endregion singleton
    // ====================================================================================================


    // ====================================================================================================
    [Header("Player Info")]
    [SerializeField] private float score;
    [SerializeField] private int coin;
    [SerializeField] private int health = 3;
    [Header("Game")]
    [SerializeField] private bool isGameReady = false;
    [SerializeField] private int gameSpeed;

    [Header("Design Level")]
    [SerializeField] private List<HutanDesignLevel> designLevelList = new List<HutanDesignLevel>();

    public int GameSpeed { get => gameSpeed; set => gameSpeed = value; }

    public bool IsGameReady { get => isGameReady; set => isGameReady = value; }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (isGameReady)
            AddScore();
    }

    public void AddScore()
    {
        score += Time.deltaTime * gameSpeed * 2.5f;
        HutanUIManager.Instance.UpdateScore(Convert.ToInt32(score));

        if (score >= designLevelList[0].onScore && designLevelList.Count > 1)
        {
            gameSpeed = designLevelList[1].gameSpeed;
            designLevelList.RemoveAt(0);
        }
    }

    public void AddCoin()
    {
        coin++;
        HutanUIManager.Instance.UpdateCoin(coin);
    }

    public void ReduceHealth()
    {
        health--;
        HutanUIManager.Instance.UpdateHealth(health);
        if (health <= 0)
        {
            isGameReady = false;
            HutanEventManager.Instance.GameOver();
            // HutanUIManager.Instance.GameOver();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

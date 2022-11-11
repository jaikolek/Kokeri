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

        Time.timeScale = 1;
    }
    #endregion singleton
    // ====================================================================================================


    // ====================================================================================================
    [Header("Player")]
    [SerializeField] private int health = 3;
    [SerializeField] private float length;
    [SerializeField] private int bug;
    [SerializeField] private int catchCounter;

    [Header("Game")]
    [SerializeField] private bool isGameReady = false;
    [SerializeField] private int gameSpeed;
    private Character character;
    private List<Character> characterList = new List<Character>() { Character.CHIKO, Character.KETTI, Character.BERI };

    [Header("Design Level")]
    [SerializeField] private List<HutanDesignLevel> designLevelList = new List<HutanDesignLevel>();


    public bool IsGameReady { get => isGameReady; set => isGameReady = value; }
    public int GameSpeed { get => gameSpeed; set => gameSpeed = value; }

    private void Start()
    {
        SceneHandler.Instance.OnSceneReloaded += SceneHandler_OnSceneReloaded;
        HutanEventManager.Instance.OnGameStarted += HutanEventManager_OnGameStarted;

        HutanEventManager.Instance.OnCharacterChanged += HutanEventManager_OnCharacterChanged;
    }

    private void SceneHandler_OnSceneReloaded()
    {
        AudioManager.Instance.StopBGM();
    }

    private void HutanEventManager_OnGameStarted()
    {
        // play audio

        catchCounter = 0;
        bug = 0;
        length = 0;
        health = 3;
    }

    private void HutanEventManager_OnCharacterChanged(Character _character)
    {
        character = _character;
    }

    private void Update()
    {
        if (isGameReady)
            IncrementLength();
    }

    public void IncrementLength()
    {
        length += Time.deltaTime * gameSpeed * 2.5f;

        if (length >= designLevelList[0].onLength && designLevelList.Count > 1)
        {
            gameSpeed = designLevelList[1].gameSpeed;
            designLevelList.RemoveAt(0);
        }
    }

    public void IncrementBug()
    {
        // show catch state (refactor this later)
        StartCoroutine(HutanUIManager.Instance.ShowState("catch"));

        // increment bug and catchCounter
        bug++;
        HutanUIManager.Instance.UpdateBug(bug);
        catchCounter++;

        // check if bug counter is more than 3
        if (catchCounter >= 3)
        {
            catchCounter = 0;

            // change to another character
            ChangeCharacter();
        }
    }

    public void ReduceHealth()
    {
        // show hit state (refactor this later)
        StartCoroutine(HutanUIManager.Instance.ShowState("hit"));

        // reduce health and update
        health--;
        HutanUIManager.Instance.UpdateHealth(health, character);

        // remove character from list
        characterList.Remove(character);

        // change to another character
        ChangeCharacter();


        // check if game over
        if (health <= 0)
        {
            CalculateResult();
            isGameReady = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangeCharacter()
    {
        if (characterList.Count > 0)
        {
            int index;
            do { index = UnityEngine.Random.Range(0, characterList.Count); } while (characterList[index] == character);

            HutanEventManager.Instance.CharacterChanged(characterList[index]);
        }
    }

    private void CalculateResult()
    {
        // 1 bug = 5 score
        int score = bug * 5;
        // 10 score = 1 coin
        int coin = score / 10;

        HutanEventManager.Instance.GameOver(score, coin, bug);
    }
}

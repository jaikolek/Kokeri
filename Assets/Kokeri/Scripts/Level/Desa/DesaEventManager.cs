using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaEventManager : MonoBehaviour
{
    #region singleton
    private static DesaEventManager instance;
    public static DesaEventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DesaEventManager>();
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
    public event Action OnGameStarted;
    public event Action<int, int> OnGameOver;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action OnPhaseStart;
    public event Action OnPlayerAnswer;
    public event Action OnTimerStopped;
    public event Action OnCorrect;
    public event Action OnWrong;
    public event Action<string, int> OnUserSubmit;

    public void GameStarted()
    {
        OnGameStarted?.Invoke();
    }
    public void GameOver(int _score, int _coin)
    {
        Time.timeScale = 0;
        
        OnGameOver?.Invoke(_score, _coin);
    }
    public void GamePaused()
    {
        Time.timeScale = 0;

        OnGamePaused?.Invoke();
    }
    public void GameResumed()
    {
        Time.timeScale = 1;

        OnGameResumed?.Invoke();
    }
    public void PhaseStart()
    {
        OnPhaseStart?.Invoke();
    }
    public void PlayerAnswer()
    {
        OnPlayerAnswer?.Invoke();
    }
    public void TimerStopped()
    {
        OnTimerStopped?.Invoke();
    }
    public void Correct()
    {
        OnCorrect?.Invoke();
    }
    public void Wrong()
    {
        OnWrong?.Invoke();
    }
    public void UserSubmit(string _name, int _score)
    {
        OnUserSubmit?.Invoke(_name, _score);
    }

    // ====================================================================================================


    // ====================================================================================================
    public event Action OnUp;
    public event Action OnDown;
    public event Action OnLeft;
    public event Action OnRight;

    public void Up()
    {
        OnUp?.Invoke();
        PlayerAnswer();
    }
    public void Down()
    {
        OnDown?.Invoke();
        PlayerAnswer();
    }
    public void Left()
    {
        OnLeft?.Invoke();
        PlayerAnswer();
    }
    public void Right()
    {
        OnRight?.Invoke();
        PlayerAnswer();
    }
}

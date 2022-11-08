using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanEventManager : MonoBehaviour
{
    #region singleton
    private static HutanEventManager instance;
    public static HutanEventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HutanEventManager>();
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
    public event Action<int, int, int> OnGameOver;
    public event Action OnDespawned;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action<Character> OnCharacterChanged;
    public event Action<bool> OnCollectRange;
    public event Action<string, int> OnUserSubmit;

    public void GameStarted()
    {
        OnGameStarted?.Invoke();
    }
    public void GameOver(int _score, int _coin, int _bug)
    {
        Time.timeScale = 0;

        OnGameOver?.Invoke(_score, _coin, _bug);
    }
    public void Despawned()
    {
        OnDespawned?.Invoke();
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
    public void CharacterChanged(Character _character)
    {
        OnCharacterChanged?.Invoke(_character);
    }
    public void CollectRange(bool _state)
    {
        OnCollectRange?.Invoke(_state);
    }
    public void UserSubmit(string _name, int _score)
    {
        OnUserSubmit?.Invoke(_name, _score);
    }
    // ====================================================================================================


    // ====================================================================================================
    public event Action OnJump;
    public event Action OnCrouch;
    public event Action OnStand;
    public event Action OnCatch;

    public void Jump()
    {
        OnJump?.Invoke();
    }
    public void Crouch()
    {
        OnCrouch?.Invoke();
    }
    public void Stand()
    {
        OnStand?.Invoke();
    }
    public void Catch()
    {
        OnCatch?.Invoke();
    }
}

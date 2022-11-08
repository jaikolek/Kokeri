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
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.transform.parent = GameObject.Find("ManagerContainer").transform;
                    obj.name = typeof(HutanEventManager).Name;
                    instance = obj.AddComponent<HutanEventManager>();
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
    public event Action OnGameStarted;
    public void GameStarted()
    {
        OnGameStarted?.Invoke();
    }

    public event Action OnDespawned;
    public void Despawned()
    {
        OnDespawned?.Invoke();
    }

    public event Action OnGamePaused;
    public void GamePaused()
    {
        Time.timeScale = 0;

        OnGamePaused?.Invoke();
    }

    public event Action OnGameResumed;
    public void GameResumed()
    {
        Time.timeScale = 1;

        OnGameResumed?.Invoke();
    }

    public event Action<int, int, int> OnGameOver;
    public void GameOver(int _score, int _coin, int _bug)
    {
        Time.timeScale = 0;

        OnGameOver?.Invoke(_score, _coin, _bug);
    }

    public event Action<string, int> OnUserSubmit;
    public void UserSubmit(string _name, int _score)
    {
        OnUserSubmit?.Invoke(_name, _score);
    }

    public event Action<Character> OnCharacterChanged;
    public void CharacterChanged(Character _character)
    {
        OnCharacterChanged?.Invoke(_character);
    }

    public event Action<bool> OnCollectRange;
    public void CollectRange(bool _state)
    {
        OnCollectRange?.Invoke(_state);
    }

    public event Action OnJump;
    public void Jump()
    {
        OnJump?.Invoke();
    }

    public event Action OnCrouch;
    public void Crouch()
    {
        OnCrouch?.Invoke();
    }

    public event Action OnStand;
    public void Stand()
    {
        OnStand?.Invoke();
    }

    public event Action OnCatch;
    public void Catch()
    {
        OnCatch?.Invoke();
    }
}

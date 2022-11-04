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

    public event Action OnGameOver;
    public void GameOver()
    {
        OnGameOver?.Invoke();
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

    public event Action<Character> OnCharacterSelected;
    public void CharacterChanged(Character _character)
    {
        OnCharacterSelected?.Invoke(_character);
        StartCoroutine(HutanUIManager.Instance.CountDown());
    }
}

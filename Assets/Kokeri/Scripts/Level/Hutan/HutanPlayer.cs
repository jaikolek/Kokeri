using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanPlayer : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController chikoAnimatorController;
    [SerializeField] private RuntimeAnimatorController kettiAnimatorController;
    [SerializeField] private RuntimeAnimatorController beriAnimatorController;

    private Animator animator;

    private void Start()
    {
        HutanEventManager.Instance.OnGameStarted += HutanEventManager_OnGameStarted;
        HutanEventManager.Instance.OnCharacterChanged += HutanEventManager_OnCharacterChanged;

        animator = GetComponent<Animator>();
    }

    private void HutanEventManager_OnGameStarted()
    {
        animator.SetBool("isGameStarted", true);

        HutanEventManager.Instance.OnGameStarted -= HutanEventManager_OnGameStarted;
    }

    private void HutanEventManager_OnCharacterChanged(Character character)
    {
        switch (character)
        {
            case Character.CHIKO:
                animator.runtimeAnimatorController = chikoAnimatorController;
                break;
            case Character.KETTI:
                // animator.runtimeAnimatorController = kettiAnimatorController;
                break;
            case Character.BERI:
                // animator.runtimeAnimatorController = beriAnimatorController;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bug"))
        {
            HutanEventManager.Instance.CollectRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bug"))
        {
            HutanEventManager.Instance.CollectRange(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            HutanGameManager.Instance.ReduceHealth();
        }
    }
}


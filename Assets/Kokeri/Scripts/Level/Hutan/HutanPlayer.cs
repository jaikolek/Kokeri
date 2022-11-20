using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanPlayer : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController chikoAnimatorController;
    [SerializeField] private RuntimeAnimatorController kettiAnimatorController;
    [SerializeField] private RuntimeAnimatorController beriAnimatorController;

    private SpriteRenderer spriteRendererComponent;
    private Animator animator;
    private bool isCooldown;

    private void Awake()
    {
        spriteRendererComponent = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        HutanEventManager.Instance.OnGameStarted += HutanEventManager_OnGameStarted;
        HutanEventManager.Instance.OnCharacterChanged += HutanEventManager_OnCharacterChanged;
        HutanEventManager.Instance.OnCatch += HutanEventManager_OnCatch;
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
                animator.runtimeAnimatorController = kettiAnimatorController;
                break;
            case Character.BERI:
                animator.runtimeAnimatorController = beriAnimatorController;
                break;
            default:
                break;
        }
    }

    private void HutanEventManager_OnCatch()
    {
        // play catch animation
        animator.SetTrigger("isCatch");
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
        if (isCooldown) return;

        if (other.gameObject.CompareTag("Obstacle"))
        {
            // play hit audio
            AudioManager.Instance.PlayHutanSFX("Hit");

            HutanGameManager.Instance.ReduceHealth();

            StartCoroutine(AfterHitCooldown());
            StartCoroutine(AfterHitSpriteBlink());
        }
    }

    private IEnumerator AfterHitCooldown()
    {
        Time.timeScale = 0;
        isCooldown = true;
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;

        yield return new WaitForSecondsRealtime(1);
        isCooldown = false;
    }

    private IEnumerator AfterHitSpriteBlink()
    {
        spriteRendererComponent.color = Color.red;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRendererComponent.color = Color.white;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRendererComponent.color = Color.red;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRendererComponent.color = Color.white;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRendererComponent.color = Color.red;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRendererComponent.color = Color.white;
    }
}


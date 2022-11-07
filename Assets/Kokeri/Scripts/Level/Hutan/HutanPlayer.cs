using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanPlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite chikoSprite;
    [SerializeField] private Sprite kettiSprite;
    [SerializeField] private Sprite beriSprite;

    private void Start()
    {
        HutanEventManager.Instance.OnCharacterChanged += HutanEventManager_OnCharacterChanged;
    }

    private void HutanEventManager_OnCharacterChanged(Character character)
    {
        switch (character)
        {
            case Character.Chiko:
                // spriteRenderer.sprite = chikoSprite;
                break;
            case Character.Ketti:
                // spriteRenderer.sprite = kettiSprite;
                break;
            case Character.Beri:
                // spriteRenderer.sprite = beriSprite;
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanParallax : MonoBehaviour
{
    private bool isStarted = false;
    [SerializeField] private float parallaxMultiplier;
    private float startPosX, spriteSizeX;


    private void Start()
    {
        HutanEventManager.Instance.OnGameStarted += HutanEventManager_OnGameStarted;

        startPosX = transform.position.x;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteSizeX = spriteRenderer.size.x / 2;
    }

    private void HutanEventManager_OnGameStarted()
    {
        isStarted = true;
    }

    private void LateUpdate()
    {
        if (isStarted)
        {
            transform.position += new Vector3(-HutanGameManager.Instance.GameSpeed * parallaxMultiplier * Time.deltaTime, 0, 0);

            if (transform.position.x < -spriteSizeX)
            {
                transform.position = new Vector3(startPosX, transform.position.y, transform.position.z);
            }
        }
    }
}


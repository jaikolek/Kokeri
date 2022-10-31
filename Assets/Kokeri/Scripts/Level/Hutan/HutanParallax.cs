using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanParallax : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier = 0.5f;
    private float startPos, bglength;
    private bool isStarted = false;

    private void Start()
    {
        HutanEventManager.Instance.OnGameStarted += HutanEventManager_OnGameStarted;

        startPos = transform.position.x;
        bglength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void HutanEventManager_OnGameStarted()
    {
        isStarted = true;
    }

    private void FixedUpdate()
    {
        if (isStarted)
        {
            transform.position = new Vector3(transform.position.x - (HutanGameManager.Instance.GameSpeed * parallaxMultiplier * Time.deltaTime), transform.position.y, transform.position.z);

            if (transform.position.x <= 0)
            {
                transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
            }
        }
    }
}

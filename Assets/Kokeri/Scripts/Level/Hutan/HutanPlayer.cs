using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanPlayer : MonoBehaviour
{
    // private void Start()
    // {
    //     HutanEventManager.Instance.OnCatch += HutanEventManager_OnCatch;
    // }

    // private void HutanEventManager_OnCatch()
    // {
        
    // }

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

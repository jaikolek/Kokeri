using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            HutanEventManager.Instance.Despawned();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Bug"))
        {
            Destroy(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanObstacle : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.left * HutanGameManager.Instance.GameSpeed * 5 * Time.deltaTime);
    }
}

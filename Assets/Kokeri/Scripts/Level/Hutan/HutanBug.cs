using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanBug : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.left * HutanGameManager.Instance.GameSpeed * 2.5f * Time.deltaTime);
    }
}

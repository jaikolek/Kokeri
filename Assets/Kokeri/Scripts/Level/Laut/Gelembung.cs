using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gelembung : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 gel = Vector3.up * Time.deltaTime * 1f;
        transform.Translate(gel);

        if (transform.position.y >= 2f)
            GameObject.Destroy(gameObject);

    }
}

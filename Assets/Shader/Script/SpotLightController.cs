using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpotLightController : MonoBehaviour
{
    [SerializeField] private Light targetLigth;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minoffset;
    [SerializeField] private float startFrom;
    [SerializeField] private float speed;

    [SerializeField] private bool highIntensity = false;

    private void Start()
    {
        targetLigth = this.gameObject.GetComponent<Light>();
        targetLigth.intensity = startFrom;
    }

    private void Update()
    {
        if (targetLigth.intensity > maxIntensity)
            highIntensity = false;
        if (targetLigth.intensity < minoffset)
            highIntensity = true;

        if (highIntensity)
            targetLigth.intensity += speed;
        if (!highIntensity)
            targetLigth.intensity -= speed;


    }
}

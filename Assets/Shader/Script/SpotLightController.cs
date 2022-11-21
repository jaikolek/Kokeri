using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpotLightController : MonoBehaviour
{
    [SerializeField] private Light2D light;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minoffset;
    [SerializeField] private float startFrom;
    [SerializeField] private float speed;

    [SerializeField] private bool highIntensity = false;

    private void Start()
    {
        light = this.gameObject.GetComponent<Light2D>();
        light.intensity = startFrom;
    }

    private void Update()
    {
        if (light.intensity > maxIntensity)
            highIntensity = false;
        if (light.intensity < minoffset)
            highIntensity = true;

        if (highIntensity)
            light.intensity += speed;
        if (!highIntensity)
            light.intensity -= speed;


    }
}

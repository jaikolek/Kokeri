using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaInGame : MonoBehaviour
{
    #region singleton
    private static DesaInGame instance;
    public static DesaInGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DesaInGame>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        rectTransform = GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale;
        defaultPosition = rectTransform.localPosition;
    }

    #endregion singleton
    // ====================================================================================================

    private RectTransform rectTransform;
    private Vector3 defaultScale;
    private Vector3 defaultPosition;

    public void ZoomToBeri()
    {
        StartCoroutine(ZoomToBeriCoroutine());
    }
    private IEnumerator ZoomToBeriCoroutine()
    {
        float time = 0;
        float duration = 0.5f;
        Vector3 targetScale = new Vector3(1.7f, 1.7f, 1.7f);
        Vector3 targetPosition = new Vector3(-800, 60, 0);
        while (time < duration)
        {
            time += Time.deltaTime;
            rectTransform.localScale = Vector3.Lerp(defaultScale, targetScale, time / duration);
            rectTransform.localPosition = Vector3.Lerp(defaultPosition, targetPosition, time / duration);
            yield return null;
        }
        rectTransform.localScale = targetScale;
        rectTransform.localPosition = targetPosition;
    }

    public void ZoomToChikoKetti()
    {
        StartCoroutine(ZoomToChikoKettiCoroutine());
    }
    private IEnumerator ZoomToChikoKettiCoroutine()
    {
        float time = 0;
        float duration = 0.5f;
        Vector3 targetScale = new Vector3(1.7f, 1.7f, 1.7f);
        Vector3 targetPosition = new Vector3(800, 60, 0);
        while (time < duration)
        {
            time += Time.deltaTime;
            rectTransform.localScale = Vector3.Lerp(defaultScale, targetScale, time / duration);
            rectTransform.localPosition = Vector3.Lerp(defaultPosition, targetPosition, time / duration);
            yield return null;
        }
        rectTransform.localScale = targetScale;
        rectTransform.localPosition = targetPosition;
    }

    public void ZoomToDefault()
    {
        StartCoroutine(ZoomToDefaultCoroutine());
    }
    private IEnumerator ZoomToDefaultCoroutine()
    {
        float time = 0;
        float duration = 0.5f;
        while (time < duration)
        {
            time += Time.deltaTime;
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, defaultScale, time / duration);
            rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, defaultPosition, time / duration);
            yield return null;
        }
        rectTransform.localScale = defaultScale;
        rectTransform.localPosition = defaultPosition;
    }
}


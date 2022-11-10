using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaCameraFocus : MonoBehaviour
{
    #region singleton
    private static DesaCameraFocus instance;
    public static DesaCameraFocus Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DesaCameraFocus>();
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
    }
    #endregion singleton
    // ====================================================================================================


    // ====================================================================================================
    [SerializeField] private Camera mainCamera;
    private Vector3 cameraPosition;
    private float cameraOrthographicSize;
    private Vector3 defaultCameraLocation;
    private float defaultCameraOrthographicSize;

    [Header("Camera Focus to Chiko Ketti")]
    [SerializeField] private Vector3 cameraPositionChikoKetti;
    [SerializeField] private float cameraOrthographicSizeChikoKetti;

    [Header("Camera Focus to Beri")]
    [SerializeField] private Vector3 cameraPositionBeri;
    [SerializeField] private float cameraOrthographicSizeBeri;

    private void Start()
    {
        defaultCameraLocation = mainCamera.transform.position;
        defaultCameraOrthographicSize = mainCamera.orthographicSize;
    }

    public void SetCameraChikoKetti()
    {
        StartCoroutine(CameraFocus(cameraPositionChikoKetti, cameraOrthographicSizeChikoKetti));
    }

    public void SetCameraBeri()
    {
        StartCoroutine(CameraFocus(cameraPositionBeri, cameraOrthographicSizeBeri));
    }

    public void SetCameraDefault()
    {
        StartCoroutine(CameraFocus(defaultCameraLocation, defaultCameraOrthographicSize));
    }

    private IEnumerator CameraFocus(Vector3 _cameraPosition, float _cameraOrthographicSize)
    {
        float elapsedTime = 0;
        float time = 1f;

        while (elapsedTime < time)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, _cameraPosition, (elapsedTime / time));
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, _cameraOrthographicSize, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}

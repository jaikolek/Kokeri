using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DesaProlog : MonoBehaviour
{
    [SerializeField] private Button skipBtn;
    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        skipBtn.onClick.AddListener(() => SkipVideo());
    }

    private void Start()
    {
        StartCoroutine(PlayVideo());
        StartCoroutine(ShowSkipBtn());
    }

    private void Update()
    {
        if (videoPlayer.time >= 5)
        {
            if (!videoPlayer.isPlaying)
            {
                SkipVideo();
            }
        }
    }

    private IEnumerator PlayVideo()
    {
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "Video/PrologDesa720p.mp4");

        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            // Debug.Log("Preparing Video");
            yield return null;
        }

        videoPlayer.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            // Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        Debug.Log("Done Playing Video");
    }

    private IEnumerator ShowSkipBtn()
    {
        yield return new WaitForSeconds(5);
        skipBtn.gameObject.SetActive(true);
    }

    private void SkipVideo()
    {
        videoPlayer.Stop();
        videoPlayer.clip = null;
        videoPlayer.enabled = false;
        gameObject.SetActive(false);

        DesaEventManager.Instance.GameStarted();
    }
}

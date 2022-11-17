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
    [SerializeField] private VideoClip videoClip;

    private void Start()
    {
        skipBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click2");

            SkipVideo();
        });

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
                return;
            }

        }

        // Debug.Log(SceneHandler.Instance.isDesaPrologPlayed);
        if (SceneHandler.Instance.isDesaPrologPlayed)
        {
            SkipVideo();
            return;
        }
    }

    private IEnumerator PlayVideo()
    {
        videoPlayer.clip = videoClip;

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
        videoPlayer.enabled = false;
        gameObject.SetActive(false);

        SceneHandler.Instance.isDesaPrologPlayed = true;

        DesaEventManager.Instance.GameStarted();
    }
}

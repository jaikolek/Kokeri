using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DesaHTP : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip videoClip;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button playBtn;

    private void Start()
    {
        backBtn.onClick.AddListener(OnClickBack);
        playBtn.onClick.AddListener(OnClickPlay);
    }

    private void OnEnable()
    {
        StartCoroutine(PlayVideo());
    }

    private void OnDisable()
    {
        videoPlayer.Stop();
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

    private void OnClickBack()
    {
        AudioManager.Instance.PlaySFX("Click2");
        gameObject.SetActive(false);
    }

    private void OnClickPlay()
    {
        AudioManager.Instance.PlaySFX("Click2");
        SceneHandler.Instance.LoadScene("LevelDesa");
    }
}

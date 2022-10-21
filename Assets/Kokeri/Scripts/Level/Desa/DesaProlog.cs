using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DesaProlog : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip videoClip;
    [SerializeField] private Button skipBtn;

    private void Start()
    {
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
        skipBtn.onClick.AddListener(() => SkipVideo());
        skipBtn.gameObject.SetActive(false);

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

        GameManagerDesa.Instance.SetCanStartGame(true);
    }
}

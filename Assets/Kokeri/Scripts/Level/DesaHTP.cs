using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DesaHTP : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip videoClip;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button playBtn;

    private void Awake()
    {
        videoPlayer.clip = videoClip;
        videoPlayer.Prepare();
    }

    private void Start()
    {
        backBtn.onClick.AddListener(OnClickBack);
        playBtn.onClick.AddListener(OnClickPlay);

    }

    private void OnEnable()
    {
        videoPlayer.Play();
    }

    private void OnDisable()
    {
        videoPlayer.Stop();
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

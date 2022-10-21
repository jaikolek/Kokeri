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
        backBtn.onClick.AddListener(OnClickBack);
        playBtn.onClick.AddListener(OnClickPlay);

        videoPlayer.clip = videoClip;
        videoPlayer.Prepare();
    }

    private void Start()
    {
        videoPlayer.Prepare();
        videoPlayer.Play();
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

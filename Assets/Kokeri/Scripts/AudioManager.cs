using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region singleton
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(AudioManager).Name;
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (AudioSourceBGM == null)
        {
            AudioSourceBGM = gameObject.AddComponent<AudioSource>();
            AudioSourceBGM.playOnAwake = true;
            AudioSourceBGM.loop = true;
        }
        if (AudioSourceSFX1 == null)
        {
            AudioSourceSFX1 = gameObject.AddComponent<AudioSource>();
            AudioSourceSFX1.playOnAwake = false;
            AudioSourceSFX1.loop = false;
        }
        if (AudioSourceSFX2 == null)
        {
            AudioSourceSFX2 = gameObject.AddComponent<AudioSource>();
            AudioSourceSFX2.playOnAwake = false;
            AudioSourceSFX2.loop = false;
        }
    }
    #endregion singleton

    [SerializeField] private float defaultVolume = 0.5f;
    [SerializeField] private List<AudioData> BGM;
    [SerializeField] private List<AudioData> mainSFX;
    [SerializeField] private List<AudioData> desaSFX;
    [SerializeField] private List<AudioData> hutanSFX;

    private AudioSource AudioSourceBGM;
    private AudioSource AudioSourceSFX1;
    private AudioSource AudioSourceSFX2;


    private void Start()
    {
        SceneHandler.Instance.OnSceneChanged += SceneHandler_SceneChanged;

        // audio init
        if (!PlayerPrefs.HasKey("BGMVolume"))
            SetBGMVolume(defaultVolume);
        else
            SetBGMVolume(GetBGMVolume());
        if (!PlayerPrefs.HasKey("SFXVolume"))
            SetSFXVolume(defaultVolume);
        else
            SetSFXVolume(GetSFXVolume());
    }

    private void SceneHandler_SceneChanged(string _sceneName)
    {
        if (_sceneName == "MainMenu" || _sceneName == "MainLevel")
        {
            if (AudioManager.Instance.IsBGMPlaying())
            {
                if (AudioManager.Instance.GetPlayingBGMName() != "MainMenu")
                {
                    AudioManager.Instance.StopBGM();
                    AudioManager.Instance.PlayBGM("MainMenu");
                }
            }
        }
        else
        {
            if (AudioManager.Instance.IsBGMPlaying())
            {
                AudioManager.Instance.StopBGM();
            }
        }
    }

    public void SetBGMVolume(float _volume)
    {
        AudioSourceBGM.volume = _volume;
        PlayerPrefs.SetFloat("BGMVolume", _volume);
    }

    public void SetSFXVolume(float _volume)
    {
        AudioSourceSFX1.volume = _volume;
        AudioSourceSFX2.volume = _volume;
        PlayerPrefs.SetFloat("SFXVolume", _volume);
    }

    public float GetBGMVolume()
    {
        return PlayerPrefs.GetFloat("BGMVolume");
    }

    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume");
    }

    public void PlayBGM(string _name)
    {
        AudioData audioData = BGM.Find(x => x.name == _name);
        if (audioData != null)
        {
            AudioSourceBGM.clip = audioData.clip;
            AudioSourceBGM.loop = audioData.loop;
            AudioSourceBGM.Play();
        }
    }

    public void PlaySFX1(string _name)
    {
        AudioData audioData = mainSFX.Find(x => x.name == _name);
        if (audioData != null)
        {
            AudioSourceSFX1.clip = audioData.clip;
            AudioSourceSFX1.Play();
        }
    }

    public void PlaySFX2(string _name)
    {
        AudioData audioData = mainSFX.Find(x => x.name == _name);
        if (audioData != null)
        {
            AudioSourceSFX2.clip = audioData.clip;
            AudioSourceSFX2.Play();
        }
    }

    public void PlayDesaSFX(string _name)
    {
        AudioData audioData = desaSFX.Find(x => x.name == _name);
        if (audioData != null)
        {
            AudioSourceSFX1.clip = audioData.clip;
            AudioSourceSFX1.Play();
        }
    }

    public void PlayHutanSFX(string _name)
    {
        AudioData audioData = hutanSFX.Find(x => x.name == _name);
        if (audioData != null)
        {
            AudioSourceSFX1.clip = audioData.clip;
            AudioSourceSFX1.Play();
        }
    }

    public string GetPlayingBGMName()
    {
        if (AudioSourceBGM.clip != null)
            return AudioSourceBGM.clip.name;
        else
            return null;
    }

    public bool IsBGMPlaying()
    {
        return AudioSourceBGM.isPlaying;
    }

    public bool IsSFX1Playing()
    {
        return AudioSourceSFX1.isPlaying;
    }

    public bool IsSFX2Playing()
    {
        return AudioSourceSFX2.isPlaying;
    }

    public void StopBGM()
    {
        AudioSourceBGM.Stop();
    }

    public void StopAllSFX()
    {
        AudioSourceSFX1.Stop();
        AudioSourceSFX2.Stop();
    }

    public void StopSFX1()
    {
        AudioSourceSFX1.Stop();
    }

    public void StopSFX2()
    {
        AudioSourceSFX1.Stop();
    }

    [Serializable]
    public struct AudioData
    {
        public string name;
        public AudioClip clip;
        public bool loop;

        // overriding
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(AudioData a, AudioData b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(AudioData a, AudioData b)
        {
            return !(a == b);
        }
    }
}

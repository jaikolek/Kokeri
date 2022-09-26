using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseStateUI : MonoBehaviour
{
    #region singleton
    private static PhaseStateUI instance;
    public static PhaseStateUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PhaseStateUI>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(PhaseStateUI).Name;
                    instance = obj.AddComponent<PhaseStateUI>();
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
        }
    }
    #endregion singleton
    // ====================================================================================================


    // ====================================================================================================
    [Header("Game Info")]
    [SerializeField] private Image infoImage;

    [Header("Countdown")]
    private int countdownTimerDefault;
    [SerializeField] private int countdownTimer;
    [SerializeField] private List<Sprite> countdownSprite;

    [Header("State")]
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite yourTurnSprite;
    [SerializeField] private Sprite gameOverSprite;
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite wrongSprite;

    [Header("Indicator Handler")]
    [SerializeField] private GameObject indicatorContainer;
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private List<Sprite> indicatorSpriteList;
    private List<GameObject> indicatorList;

    private void Start()
    {
        SetInfoImage(startSprite);
        countdownTimerDefault = countdownTimer;
        indicatorList = new List<GameObject>();
    }

    public void SetInfoImage(Sprite _sprite)
    {
        infoImage.sprite = _sprite;
    }

    public IEnumerator StartCountdown()
    {
        if (!infoImage.gameObject.activeSelf)
            infoImage.gameObject.SetActive(true);

        while (countdownTimer > 0)
        {
            SetInfoImage(countdownSprite[countdownTimer - 1]);
            yield return new WaitForSeconds(1f);
            countdownTimer--;
        }

        SetInfoImage(startSprite);
        yield return new WaitForSeconds(1f);
        infoImage.gameObject.SetActive(false);

        GameManagerDesa.Instance.SetIsGameReady(true);

        countdownTimer = countdownTimerDefault;
    }

    public IEnumerator ShowState(Sprite _sprite)
    {
        if (!infoImage.gameObject.activeSelf)
            infoImage.gameObject.SetActive(true);

        SetInfoImage(_sprite);
        yield return new WaitForSeconds(1f);
        infoImage.gameObject.SetActive(false);
    }

    public void ShowStartState()
    {
        StartCoroutine(ShowState(startSprite));
    }

    public void ShowYourTurnState()
    {
        StartCoroutine(ShowState(yourTurnSprite));
    }

    public void ShowGameOverState()
    {
        StartCoroutine(ShowState(gameOverSprite));
    }

    public void ShowCorrectState()
    {
        StartCoroutine(ShowState(correctSprite));
    }

    public void ShowWrongState()
    {
        StartCoroutine(ShowState(wrongSprite));
    }
    // ====================================================================================================


    // ====================================================================================================
    public void AddIndicator(MoveType _moveType)
    {
        GameObject indicator = Instantiate(indicatorPrefab, indicatorContainer.transform);
        indicator.GetComponent<Image>().sprite = indicatorSpriteList[Convert.ToInt32(_moveType) - 1];

        indicatorList.Add(indicator);
    }

    public void RemoveAllIndicator()
    {
        foreach (GameObject indicator in indicatorList)
        {
            Destroy(indicator);
        }
        indicatorList.Clear();
    }
}

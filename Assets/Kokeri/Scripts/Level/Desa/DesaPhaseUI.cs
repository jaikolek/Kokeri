using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesaPhaseUI : MonoBehaviour
{
    #region singleton
    private static DesaPhaseUI instance;
    public static DesaPhaseUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DesaPhaseUI>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(DesaPhaseUI).Name;
                    instance = obj.AddComponent<DesaPhaseUI>();
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
    [Header("Container")]
    [SerializeField] private Image infoImage;

    [Header("Countdown")]
    private int countdownTimerDefault;
    [SerializeField] private int countdownTimer;
    [SerializeField] private List<Sprite> countdownSprite;

    [Header("State")]
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite yourTurnSprite;
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite wrongSprite;
    [SerializeField] private GameObject gameOverPopUp;

    [Header("Indicator Handler")]
    [SerializeField] private GameObject indicatorContainer;
    private Image indicatorContainerImage;
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private List<Sprite> indicatorSpriteList;
    private List<GameObject> indicatorList;

    private bool isStateRunning;

    private void Start()
    {
        SetInfoImage(startSprite);
        countdownTimerDefault = countdownTimer;
        indicatorList = new List<GameObject>();
        indicatorContainerImage = indicatorContainer.GetComponent<Image>();
    }

    public void SetInfoImage(Sprite _sprite)
    {
        infoImage.sprite = _sprite;
    }

    public IEnumerator StartCountdown()
    {
        AudioManager.Instance.PlayBGM("Desa");
        yield return new WaitForSeconds(1.5f);

        if (!infoImage.gameObject.activeSelf)
            infoImage.gameObject.SetActive(true);

        while (countdownTimer > 0)
        {
            SetInfoImage(countdownSprite[countdownTimer - 1]);
            yield return new WaitForSeconds(1f);
            countdownTimer--;
        }

        infoImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);

        if (!GameManagerDesa.Instance.GetIsGameReady())
            GameManagerDesa.Instance.SetIsGameReady(true);

        countdownTimer = countdownTimerDefault;
    }

    public IEnumerator ShowState(Sprite _sprite)
    {
        SetIsStateRunning(true);


        if (!infoImage.gameObject.activeSelf)
            infoImage.gameObject.SetActive(true);

        SetInfoImage(_sprite);
        yield return new WaitForSeconds(delayTime);

        infoImage.gameObject.SetActive(false);
        indicatorContainerImage.enabled = false;

        SetIsStateRunning(false);
    }

    public IEnumerator ShowGameOver()
    {
        SetIsStateRunning(true);


        if (!gameOverPopUp.activeSelf)
            gameOverPopUp.SetActive(true);

        yield return new WaitForSeconds(delayTime);

        gameOverPopUp.SetActive(false);

        SetIsStateRunning(false);
    }

    public void ShowStartState()
    {
        if (GetIsStateRunning()) return;

        StartCoroutine(ShowState(startSprite));
    }

    public void ShowYourTurnState()
    {
        if (GetIsStateRunning()) return;

        StartCoroutine(ShowState(yourTurnSprite));
    }

    public void ShowGameOverState()
    {
        if (GetIsStateRunning()) return;

        StartCoroutine(ShowGameOver());
    }

    public void ShowCorrectState()
    {
        if (GetIsStateRunning()) return;

        StartCoroutine(ShowState(correctSprite));
    }

    public void ShowWrongState()
    {
        if (GetIsStateRunning()) return;

        StartCoroutine(ShowState(wrongSprite));
    }

    public void ShowIndicatorContainer()
    {
        indicatorContainerImage.enabled = true;
    }

    public void HideIndicatorContainer()
    {
        indicatorContainerImage.enabled = false;
    }
    // ====================================================================================================


    // ====================================================================================================
    public void AddIndicator(MoveType _moveType)
    {
        GameObject indicator = Instantiate(indicatorPrefab, indicatorContainer.transform);
        indicator.GetComponent<Image>().sprite = indicatorSpriteList[Convert.ToInt32(_moveType) - 1];

        if (indicatorList.Count % 2 == 0)
            indicator.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.8f);
        else
            indicator.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.2f);


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
    // ====================================================================================================


    // ====================================================================================================
    public void SetIsStateRunning(bool _isStateRunning)
    {
        isStateRunning = _isStateRunning;
    }

    public bool GetIsStateRunning()
    {
        return isStateRunning;
    }
}

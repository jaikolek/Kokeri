using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesaUIManager : MonoBehaviour
{
    #region singleton
    private static DesaUIManager instance;
    public static DesaUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DesaUIManager>();
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
    [Header("UI")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOverPopUp;
    [SerializeField] private GameObject pausePopUp;
    [SerializeField] private GameObject scoreBoardPopUp;

    [Header("Input")]
    [SerializeField] private Button upBtn;
    [SerializeField] private Button downBtn;
    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button pauseBtn;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Health")]
    [SerializeField] private GameObject healthContainer;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("State")]
    [SerializeField] private Image statePanelImage;
    [SerializeField] private Sprite caseSprite;
    [SerializeField] private Sprite answerSprite;
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite wrongSprite;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private List<Sprite> countdownSprite;

    [Header("Move Indicator")]
    [SerializeField] private GameObject moveIndicatorPrefab;
    [SerializeField] private GameObject moveIndicatorContainer;
    [SerializeField] private List<Sprite> moveIndicatorSpriteList;
    [SerializeField] private List<Sprite> moveIndicatorActiveSpriteList;
    private List<GameObject> moveIndicatorList = new List<GameObject>();

    private void Start()
    {
        DesaEventManager.Instance.OnGameStarted += DesaEventManager_OnGameStarted;
        DesaEventManager.Instance.OnGamePaused += DesaEventManager_OnGamePaused;
        DesaEventManager.Instance.OnGameResumed += DesaEventManager_OnGameResumed;
        DesaEventManager.Instance.OnGameOver += DesaEventManager_OnGameOver;

        DesaEventManager.Instance.OnUserSubmit += DesaEventManager_OnUserSubmit;

        pauseBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click2");

            DesaEventManager.Instance.GamePaused();
        });

        upBtn.onClick.AddListener(() => DesaEventManager.Instance.Up());
        downBtn.onClick.AddListener(() => DesaEventManager.Instance.Down());
        leftBtn.onClick.AddListener(() => DesaEventManager.Instance.Left());
        rightBtn.onClick.AddListener(() => DesaEventManager.Instance.Right());

        DisableButton();
    }

    private void DesaEventManager_OnGameStarted()
    {
        gameUI.SetActive(true);
        StartCoroutine(Countdown());
    }

    private void DesaEventManager_OnGamePaused()
    {
        pausePopUp.SetActive(true);
    }

    private void DesaEventManager_OnGameResumed()
    {
        pausePopUp.SetActive(false);
    }

    private void DesaEventManager_OnGameOver(int _score, int _coin)
    {
        StartCoroutine(ShowGameOver(_score, _coin));

        DesaEventManager.Instance.OnGamePaused -= DesaEventManager_OnGamePaused;
        DesaEventManager.Instance.OnGameResumed -= DesaEventManager_OnGameResumed;
    }

    private void DesaEventManager_OnUserSubmit(string _name, int _score)
    {
        scoreBoardPopUp.SetActive(true);
        scoreBoardPopUp.GetComponent<ScoreBoardPopUp>().ShowResultDesa(_name, _score);
    }

    private IEnumerator Countdown()
    {
        statePanelImage.gameObject.SetActive(true);
        for (int i = 0; i < countdownSprite.Count; i++)
        {
            statePanelImage.sprite = countdownSprite[i];
            yield return new WaitForSeconds(1f);
        }
        statePanelImage.gameObject.SetActive(false);

        DesaEventManager.Instance.PhaseStart();
    }

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score : " + _score.ToString();
    }

    public void UpdateHealth(int _health)
    {
        healthText.text = _health.ToString() + "X";

        healthContainer.transform.GetChild(_health).gameObject.SetActive(false);
    }

    public IEnumerator ShowState(DStateType _stateType, Action _callback = null)
    {
        statePanelImage.gameObject.SetActive(true);
        switch (_stateType)
        {
            case DStateType.CASE:
                statePanelImage.sprite = caseSprite;
                break;
            case DStateType.ANSWER:
                statePanelImage.sprite = answerSprite;
                break;
            case DStateType.CORRECT:
                statePanelImage.sprite = correctSprite;
                break;
            case DStateType.WRONG:
                statePanelImage.sprite = wrongSprite;
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(1);
        statePanelImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

        if (_callback != null)
        {
            _callback();
        }
    }

    private IEnumerator ShowGameOver(int _score, int _coin)
    {
        DesaEventManager.Instance.GameResumed();

        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        gameOverPanel.SetActive(false);

        DesaEventManager.Instance.GamePaused();

        gameOverPopUp.SetActive(true);
        gameOverPopUp.GetComponent<DesaGameOverPopUp>().ShowResult(_score, _coin);
    }

    public void ShowMoveIndicatorContainer()
    {
        moveIndicatorContainer.SetActive(true);
    }

    public void HideMoveIndicatorContainer()
    {
        moveIndicatorContainer.SetActive(false);
    }

    public void EnableButton()
    {
        upBtn.interactable = true;
        downBtn.interactable = true;
        leftBtn.interactable = true;
        rightBtn.interactable = true;
    }

    public void DisableButton()
    {
        upBtn.interactable = false;
        downBtn.interactable = false;
        leftBtn.interactable = false;
        rightBtn.interactable = false;
    }

    public void AddActiveMoveIndicator(DMoveType _moveType)
    {
        GameObject moveIndicator = Instantiate(moveIndicatorPrefab, moveIndicatorContainer.transform);
        moveIndicator.GetComponent<Image>().sprite = moveIndicatorActiveSpriteList[Convert.ToInt32(_moveType) - 1];
        moveIndicatorList.Add(moveIndicator);

        if (moveIndicatorList.Count % 2 == 0)
            moveIndicator.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.8f);
        else
            moveIndicator.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.2f);
    }

    public void AddMoveIndicator(DMoveType _moveType)
    {
        GameObject moveIndicator = Instantiate(moveIndicatorPrefab, moveIndicatorContainer.transform);
        moveIndicator.GetComponent<Image>().sprite = moveIndicatorSpriteList[Convert.ToInt32(_moveType) - 1];
        moveIndicatorList.Add(moveIndicator);

        if (moveIndicatorList.Count % 2 == 0)
            moveIndicator.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.8f);
        else
            moveIndicator.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.2f);
    }

    public void ChangeToActiveMoveIndicator(int _index, DMoveType _moveType)
    {
        moveIndicatorList[_index].GetComponent<Image>().sprite = moveIndicatorActiveSpriteList[Convert.ToInt32(_moveType) - 1];
    }

    public void RemoveAllMoveIndicator()
    {
        foreach (var item in moveIndicatorList)
        {
            Destroy(item);
        }
        moveIndicatorList.Clear();
    }
}

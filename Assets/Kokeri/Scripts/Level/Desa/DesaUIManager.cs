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

        pauseBtn.onClick.AddListener(() => DesaEventManager.Instance.GamePaused());

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

    public void UpdateScore(int score)
    {
        scoreText.text = "Score : " + score.ToString();
    }

    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString() + "X";

        healthContainer.transform.GetChild(health - 1).gameObject.SetActive(false);
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

    public IEnumerator ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        gameOverPanel.SetActive(false);
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

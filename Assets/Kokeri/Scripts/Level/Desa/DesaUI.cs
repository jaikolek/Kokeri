using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesaUI : MonoBehaviour
{
    #region singleton
    private static DesaUI instance;
    public static DesaUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DesaUI>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(DesaUI).Name;
                    instance = obj.AddComponent<DesaUI>();
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
    [Header("Pause")]
    [SerializeField] private Button pauseBtn;
    [SerializeField] private GameObject pausePopUp;

    [Header("Control Handler")]
    [SerializeField] private Button upBtn;
    [SerializeField] private Button downBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button leftBtn;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Health")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Transform healthIndicatorContainer;
    [SerializeField] private GameObject healthIndicatorPrefab;
    private List<GameObject> healthIndicatorList;

    private void Start()
    {
        pauseBtn.onClick.AddListener(OnClickPause);
        upBtn.onClick.AddListener(OnClickUp);
        downBtn.onClick.AddListener(OnClickDown);
        leftBtn.onClick.AddListener(OnClickLeft);
        rightBtn.onClick.AddListener(OnClickRight);

        healthIndicatorList = new List<GameObject>();

        InitHealthIndicator(GameManagerDesa.Instance.GetPlayerHealth());
    }

    private void Update()
    {
        if (GameManagerDesa.Instance.GetIsPlayerTurn())
        {
            SetActiveInputControl(true);
        }
        else
        {
            SetActiveInputControl(false);
        }
    }

    private void SetActiveInputControl(bool _bool)
    {
        upBtn.gameObject.SetActive(_bool);
        downBtn.gameObject.SetActive(_bool);
        leftBtn.gameObject.SetActive(_bool);
        rightBtn.gameObject.SetActive(_bool);
    }
    // ====================================================================================================

    // health
    // ====================================================================================================
    private void InitHealthIndicator(int _health)
    {
        for (int i = 0; i < _health; i++)
        {
            GameObject healthIndicator = Instantiate(healthIndicatorPrefab, healthIndicatorContainer);
            healthIndicatorList.Add(healthIndicator);
        }
    }

    public void UpdateHealth(int _health)
    {
        UpdateHealthIndicator(_health);
        UpdateHealthText(_health);
    }

    public void UpdateHealthIndicator(int _health)
    {
        for (int i = 0; i < healthIndicatorList.Count; i++)
        {
            if (i < _health)
            {
                healthIndicatorList[i].SetActive(true);
            }
            else
            {
                healthIndicatorList[i].SetActive(false);
            }
        }
    }

    public void UpdateHealthText(int _health)
    {
        healthText.text = _health.ToString() + "X";
    }
    // ====================================================================================================

    // score
    // ====================================================================================================
    public void UpdateScore(int _score)
    {
        scoreText.text = "SCORE : " + _score.ToString();
    }
    // ====================================================================================================

    // on click
    // ====================================================================================================
    private void OnClickPause()
    {
        AudioManager.Instance.PlaySFX("Click1");
        GameManagerDesa.Instance.HandlePause();
        pausePopUp.SetActive(true);
    }

    private void OnClickUp()
    {
        AudioManager.Instance.PlaySFX("Click2");
        GameManagerDesa.Instance.HandleUpInput();
    }

    private void OnClickDown()
    {
        AudioManager.Instance.PlaySFX("Click2");
        GameManagerDesa.Instance.HandleDownInput();
    }

    private void OnClickLeft()
    {
        AudioManager.Instance.PlaySFX("Click2");
        GameManagerDesa.Instance.HandleLeftInput();
    }

    private void OnClickRight()
    {
        AudioManager.Instance.PlaySFX("Click2");
        GameManagerDesa.Instance.HandleRightInput();
    }
}

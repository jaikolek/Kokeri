using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HutanUIManager : MonoBehaviour
{
    #region singleton
    private static HutanUIManager instance;
    public static HutanUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HutanUIManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.transform.parent = GameObject.Find("ManagerContainer").transform;
                    obj.name = typeof(HutanUIManager).Name;
                    instance = obj.AddComponent<HutanUIManager>();
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
    [Header("UI")]
    [SerializeField] private GameObject pausePopUp;
    [SerializeField] private GameObject gameOverPopUp;
    [SerializeField] private TextMeshProUGUI countDownText;

    [Header("Input")]
    [SerializeField] private Button upBtn;
    [SerializeField] private Button downBtn;
    [SerializeField] private Button catchBtn;

    [Header("Score")]
    [SerializeField] private GameObject score;

    [Header("Coin")]
    [SerializeField] private GameObject coin;

    [Header("Health")]
    [SerializeField] private GameObject health;

    private void Start()
    {
        HutanEventManager.Instance.OnCollectRange += HutanEventManager_CollectRange;
        HutanEventManager.Instance.OnGameOver += HutanEventManager_GameOver;

        upBtn.GetComponent<ButtonPointerDownListener>().onPointerDown.AddListener(() => HutanEventManager.Instance.Jump());

        downBtn.GetComponent<ButtonPointerDownListener>().onPointerDown.AddListener(() => HutanEventManager.Instance.Crouch());
        downBtn.GetComponent<ButtonPointerUpListener>().onPointerUp.AddListener(() => HutanEventManager.Instance.Stand());

        catchBtn.GetComponent<ButtonPointerDownListener>().onPointerDown.AddListener(() => HutanEventManager.Instance.Catch());

        catchBtn.interactable = false;
        StartCoroutine(CountDown());
    }

    private void HutanEventManager_CollectRange(bool _state)
    {
        if (_state)
        {
            catchBtn.interactable = true;
        }
        else
        {
            catchBtn.interactable = false;
        }
    }

    private void HutanEventManager_GameOver()
    {
        Time.timeScale = 0;
        gameOverPopUp.SetActive(true);
    }

    private IEnumerator CountDown()
    {
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "GO";
        yield return new WaitForSeconds(1);
        countDownText.gameObject.SetActive(false);

        HutanGameManager.Instance.IsGameReady = true;
        HutanEventManager.Instance.GameStarted();
    }

    public void UpdateScore(int score)
    {
        this.score.GetComponentInChildren<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

    public void UpdateCoin(int coin)
    {
        this.coin.GetComponentInChildren<TextMeshProUGUI>().text = "Kumbang: " + coin.ToString();
    }

    public void UpdateHealth(int health)
    {
        this.health.GetComponentInChildren<TextMeshProUGUI>().text = "Health: " + health.ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDesa : MonoBehaviour
{
    #region singleton
    private static GameManagerDesa instance;
    public static GameManagerDesa Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManagerDesa>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManagerDesa).Name;
                    instance = obj.AddComponent<GameManagerDesa>();
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
    private MoveInventory MoveCase;
    private MoveInventory MoveAnswer;

    private bool isGameInitiated = false;
    private bool isGameReady = false;
    private bool isPhaseRunning = false;
    private bool isAnimationRunning = false;
    private bool isPlayerTurn = false;
    private bool isPlayerCorrect = false;

    [Header("Player Info")]
    [SerializeField] private int playerHealth = 3;
    private int playerScore;

    [Header("Game Info")]
    [SerializeField] private float waitDelay = 1f;
    [SerializeField] private float moveDelay = 1f;
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentCase;

    [Header("Level Design")]
    [SerializeField] private List<LevelDesignDesa> levelDesignDesaList;

    [Header("Animator")]
    [SerializeField] private Animator chikoAnimator;
    [SerializeField] private Animator kettiAnimator;
    [SerializeField] private Animator beriAnimator;

    private void Start()
    {
        MoveCase = new MoveInventory();
        MoveAnswer = new MoveInventory();
    }

    private void Update()
    {
        // start countdown
        if (!isGameInitiated)
        {
            playerScore = 0;
            currentLevel = 0;
            currentCase = 0;

            DesaUI.Instance.UpdateHealth(playerHealth);
            DesaUI.Instance.UpdateScore(playerScore);

            StartCoroutine(DesaPhaseUI.Instance.StartCountdown());
            SetIsGameInitiated(true);
        }

        if (isGameReady && !isPhaseRunning)
        {
            SetIsPhaseRunning(true);

            // chiko and ketti turn
            if (!isAnimationRunning && !isPlayerTurn)
            {
                MakeCase();
                StartCoroutine(ShowCase());
            }

            // beri turn
            if (!isAnimationRunning && isPlayerTurn)
            {
                HandlePlayerAnswer();

                if (MoveCase.GetMoveList().Count == MoveAnswer.GetMoveList().Count && GetIsPlayerCorrect())
                {
                    HandleCorrectAnswer();
                }
            }

            SetIsPhaseRunning(false);
        }
    }

    private void MakeCase()
    {
        for (int i = 0; i < levelDesignDesaList.Count; i++)
        {
            if (levelDesignDesaList[i].onCase == currentCase)
            {
                currentLevel = i;
            }
        }

        MoveCase.CreateMoveCase(levelDesignDesaList[currentLevel].totalMove, levelDesignDesaList[currentLevel].caseType);
    }

    private IEnumerator ShowCase()
    {
        SetIsAnimationRunning(true);

        DesaCameraFocus.Instance.SetCameraChikoKetti();

        DesaPhaseUI.Instance.ShowStartState();
        yield return new WaitForSeconds(waitDelay);

        for (int i = 0; i < MoveCase.GetMoveListCount(); i++)
        {
            chikoAnimator.SetBool("Move", true);
            kettiAnimator.SetBool("Move", true);

            DesaPhaseUI.Instance.AddIndicator(MoveCase.GetMoveType(i));
            chikoAnimator.SetFloat("Direction", Convert.ToInt32(MoveCase.GetMoveType(i)));
            kettiAnimator.SetFloat("Direction", Convert.ToInt32(MoveCase.GetMoveType(i)));

            yield return new WaitForSeconds(moveDelay);

            chikoAnimator.SetBool("Move", false);
            kettiAnimator.SetBool("Move", false);

            yield return new WaitForSeconds(moveDelay * 0.25f);
        }

        DesaPhaseUI.Instance.RemoveAllIndicator();

        DesaPhaseUI.Instance.ShowYourTurnState();
        yield return new WaitForSeconds(waitDelay);
        SetIsPlayerTurn(true);

        DesaCameraFocus.Instance.SetCameraDefault();

        SetIsAnimationRunning(false);
    }

    private void HandlePlayerAnswer()
    {
        SetIsPlayerCorrect(true);

        for (int i = 0; i < MoveAnswer.GetMoveListCount(); i++)
        {
            if (MoveCase.GetMoveType(i) == MoveAnswer.GetMoveType(i))
            {
                SetIsPlayerCorrect(true);
            }
            else
            {
                SetIsPlayerCorrect(false);
                break;
            }
        }

        if (!GetIsPlayerCorrect())
        {
            HandleWrongAnswer();

            playerHealth--;
            DesaUI.Instance.UpdateHealth(playerHealth);
        }
    }

    private void HandleCorrectAnswer()
    {
        DesaPhaseUI.Instance.RemoveAllIndicator();
        DesaPhaseUI.Instance.ShowCorrectState();

        StartCoroutine(ShowCorrectAnimation());

        playerScore += levelDesignDesaList[currentLevel].score;
        DesaUI.Instance.UpdateScore(playerScore);

        currentCase++;
    }

    private void HandleWrongAnswer()
    {
        DesaPhaseUI.Instance.RemoveAllIndicator();
        DesaPhaseUI.Instance.ShowWrongState();

        StartCoroutine(ShowWrongAnimation());
    }

    private IEnumerator ShowCorrectAnimation()
    {
        SetIsAnimationRunning(true);
        SetIsPlayerTurn(false);

        beriAnimator.SetFloat("State", 2);

        for (int i = 0; i < MoveCase.GetMoveListCount(); i++)
        {
            beriAnimator.SetBool("Move", true);

            beriAnimator.SetFloat("Direction", Convert.ToInt32(MoveCase.GetMoveType(i)));

            yield return new WaitForSeconds(moveDelay);

            beriAnimator.SetBool("Move", false);

            yield return new WaitForSeconds(moveDelay * 0.25f);
        }

        beriAnimator.SetFloat("State", 1);

        MoveCase.ClearMoveList();
        MoveAnswer.ClearMoveList();

        SetIsAnimationRunning(false);
    }

    private IEnumerator ShowWrongAnimation()
    {
        SetIsAnimationRunning(true);
        SetIsPlayerTurn(false);

        yield return new WaitForSeconds(waitDelay);

        MoveCase.ClearMoveList();
        MoveAnswer.ClearMoveList();

        SetIsAnimationRunning(false);
    }
    // ====================================================================================================

    // player input handler
    // ====================================================================================================
    public void HandlePause()
    {
        Time.timeScale = 0f;
    }

    public void HandleUpInput()
    {
        MoveAnswer.AddMove(new Move(MoveType.UP));
        DesaPhaseUI.Instance.AddIndicator(MoveType.UP);
    }

    public void HandleDownInput()
    {
        MoveAnswer.AddMove(new Move(MoveType.DOWN));
        DesaPhaseUI.Instance.AddIndicator(MoveType.DOWN);
    }

    public void HandleLeftInput()
    {
        MoveAnswer.AddMove(new Move(MoveType.LEFT));
        DesaPhaseUI.Instance.AddIndicator(MoveType.LEFT);
    }

    public void HandleRightInput()
    {
        MoveAnswer.AddMove(new Move(MoveType.RIGHT));
        DesaPhaseUI.Instance.AddIndicator(MoveType.RIGHT);
    }
    // ====================================================================================================

    // setter getter
    // ====================================================================================================
    public int GetPlayerHealth()
    {
        return playerHealth;
    }
    // ====================================================================================================

    // boolean setter getter
    // ====================================================================================================
    public void SetIsGameInitiated(bool _isGameInitiated)
    {
        isGameInitiated = _isGameInitiated;
    }
    public bool GetIsGameInitiated()
    {
        return isGameInitiated;
    }

    public void SetIsGameReady(bool _isGameReady)
    {
        isGameReady = _isGameReady;
    }
    public bool GetIsGameReady()
    {
        return isGameReady;
    }

    public void SetIsPhaseRunning(bool _isCaseRunning)
    {
        isPhaseRunning = _isCaseRunning;
    }
    public bool GetIsPhaseRunning()
    {
        return isPhaseRunning;
    }

    public void SetIsAnimationRunning(bool _isAnimationRunning)
    {
        isAnimationRunning = _isAnimationRunning;
    }
    public bool GetIsAnimationRunning()
    {
        return isAnimationRunning;
    }

    public void SetIsPlayerTurn(bool _isPlayerTurn)
    {
        isPlayerTurn = _isPlayerTurn;
    }
    public bool GetIsPlayerTurn()
    {
        return isPlayerTurn;
    }

    public void SetIsPlayerCorrect(bool _isPlayerCorrect)
    {
        isPlayerCorrect = _isPlayerCorrect;
    }

    public bool GetIsPlayerCorrect()
    {
        return isPlayerCorrect;
    }
}

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

    [SerializeField] private float waitDelay = 1f;
    [SerializeField] private float moveDelay = 1f;
    private int currentLevel = 0;
    private int currentCase = 0;

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
            StartCoroutine(PhaseStateUI.Instance.StartCountdown());
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
                break;
            }
        }

        MoveCase.CreateMoveCase(levelDesignDesaList[currentLevel].totalMove, levelDesignDesaList[currentLevel].caseType);
    }

    private IEnumerator ShowCase()
    {
        SetIsAnimationRunning(true);

        PhaseStateUI.Instance.ShowStartState();
        yield return new WaitForSeconds(waitDelay);

        for (int i = 0; i < MoveCase.GetMoveListCount(); i++)
        {
            chikoAnimator.SetBool("Move", true);
            kettiAnimator.SetBool("Move", true);

            PhaseStateUI.Instance.AddIndicator(MoveCase.GetMoveType(i));
            chikoAnimator.SetFloat("Direction", Convert.ToInt32(MoveCase.GetMoveType(i)));
            kettiAnimator.SetFloat("Direction", Convert.ToInt32(MoveCase.GetMoveType(i)));

            yield return new WaitForSeconds(moveDelay);

            chikoAnimator.SetBool("Move", false);
            kettiAnimator.SetBool("Move", false);

            yield return new WaitForSeconds(moveDelay * 0.25f);
        }

        PhaseStateUI.Instance.RemoveAllIndicator();

        PhaseStateUI.Instance.ShowYourTurnState();
        yield return new WaitForSeconds(waitDelay);
        SetIsPlayerTurn(true);

        SetIsAnimationRunning(false);
    }

    private void HandlePlayerAnswer()
    {
        for (int i = 0; i < MoveAnswer.GetMoveListCount(); i++)
        {
            if (MoveCase.GetMoveType(i) == MoveAnswer.GetMoveType(i))
            {
                HandleCorrectAnswer();
            }
            else
            {
                HandleWrongAnswer();
            }
        }
    }

    private void HandleCorrectAnswer()
    {
        if (MoveCase.GetMoveListCount() == MoveAnswer.GetMoveListCount())
        {
            PhaseStateUI.Instance.RemoveAllIndicator();
            PhaseStateUI.Instance.ShowCorrectState();

            StartCoroutine(ShowCorrect());

            currentCase++;
        }
    }

    private void HandleWrongAnswer()
    {
        PhaseStateUI.Instance.RemoveAllIndicator();
        PhaseStateUI.Instance.ShowWrongState();

        StartCoroutine(ShowWrong());
    }

    private IEnumerator ShowCorrect()
    {
        SetIsAnimationRunning(true);
        SetIsPlayerTurn(false);

        for (int i = 0; i < MoveCase.GetMoveListCount(); i++)
        {
            beriAnimator.SetBool("Move", true);

            beriAnimator.SetFloat("Direction", Convert.ToInt32(MoveCase.GetMoveType(i)));

            yield return new WaitForSeconds(moveDelay);

            beriAnimator.SetBool("Move", false);

            yield return new WaitForSeconds(moveDelay * 0.25f);
        }

        MoveCase.ClearMoveList();
        MoveAnswer.ClearMoveList();

        SetIsAnimationRunning(false);
    }

    private IEnumerator ShowWrong()
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
        PhaseStateUI.Instance.AddIndicator(MoveType.UP);
    }

    public void HandleDownInput()
    {
        MoveAnswer.AddMove(new Move(MoveType.DOWN));
        PhaseStateUI.Instance.AddIndicator(MoveType.DOWN);
    }

    public void HandleLeftInput()
    {
        MoveAnswer.AddMove(new Move(MoveType.LEFT));
        PhaseStateUI.Instance.AddIndicator(MoveType.LEFT);
    }

    public void HandleRightInput()
    {
        MoveAnswer.AddMove(new Move(MoveType.RIGHT));
        PhaseStateUI.Instance.AddIndicator(MoveType.RIGHT);
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
}

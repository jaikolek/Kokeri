using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaGameManager : MonoBehaviour
{
    #region singleton
    private static DesaGameManager instance;
    public static DesaGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DesaGameManager>();
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

        Time.timeScale = 1;
    }
    #endregion singleton
    // ====================================================================================================


    // ====================================================================================================
    private IEnumerator timerCoroutine;
    private IEnumerator showTimeUpCountdownCoroutine;
    private DesaMoveInventory moveCase;
    private DesaMoveInventory moveAnswer;

    [Header("Player")]
    [SerializeField] private int currentCase = 1;
    [SerializeField] private int currentLevel;
    [SerializeField] private int score;
    [SerializeField] private int health = 3;

    [Header("Game")]
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private float animationDelay = 0.5f;


    [Header("Design Level")]
    [SerializeField] private List<DesaDesignLevel> designLevelList = new List<DesaDesignLevel>();

    [Header("Animator")]
    [SerializeField] private Animator chikoAnimator;
    [SerializeField] private Animator kettiAnimator;
    [SerializeField] private Animator beriAnimator;

    private void Start()
    {
        SceneHandler.Instance.OnSceneReloaded += SceneHandler_OnSceneReloaded;
        DesaEventManager.Instance.OnGameStarted += DesaEventManager_OnGameStarted;

        DesaEventManager.Instance.OnUp += DesaEventManager_OnUp;
        DesaEventManager.Instance.OnDown += DesaEventManager_OnDown;
        DesaEventManager.Instance.OnLeft += DesaEventManager_OnLeft;
        DesaEventManager.Instance.OnRight += DesaEventManager_OnRight;

        DesaEventManager.Instance.OnPhaseStart += DesaEventManager_OnPhaseStart;
        DesaEventManager.Instance.OnPlayerAnswer += DesaEventManager_OnPlayerAnswer;
        DesaEventManager.Instance.OnCorrect += DesaEventManager_OnCorrect;
        DesaEventManager.Instance.OnWrong += DesaEventManager_OnWrong;

        moveCase = new DesaMoveInventory();
        moveAnswer = new DesaMoveInventory();
    }

    private void SceneHandler_OnSceneReloaded()
    {
        AudioManager.Instance.StopBGM();
    }

    private void DesaEventManager_OnGameStarted()
    {
        // play audio
        AudioManager.Instance.PlayBGM("Desa");

        currentCase = 1;
        currentLevel = 0;
        score = 0;
        health = 3;
    }

    private void DesaEventManager_OnUp()
    {
        moveAnswer.AddMove(new DesaMove(DMoveType.UP));
        DesaUIManager.Instance.AddMoveIndicator(DMoveType.UP);
    }

    private void DesaEventManager_OnDown()
    {
        moveAnswer.AddMove(new DesaMove(DMoveType.DOWN));
        DesaUIManager.Instance.AddMoveIndicator(DMoveType.DOWN);
    }

    private void DesaEventManager_OnLeft()
    {
        moveAnswer.AddMove(new DesaMove(DMoveType.LEFT));
        DesaUIManager.Instance.AddMoveIndicator(DMoveType.LEFT);
    }

    private void DesaEventManager_OnRight()
    {
        moveAnswer.AddMove(new DesaMove(DMoveType.RIGHT));
        DesaUIManager.Instance.AddMoveIndicator(DMoveType.RIGHT);
    }

    private void DesaEventManager_OnPhaseStart()
    {
        DesaUIManager.Instance.DisableButton();

        StartCoroutine(DesaUIManager.Instance.ShowState(DStateType.CASE, () =>
        {
            MakeCase();
            moveAnswer.ClearMoveList();

            StartCoroutine(PlayPhase());
        }));
    }

    private void DesaEventManager_OnPlayerAnswer()
    {
        if (moveAnswer.GetMoveListCount() == moveCase.GetMoveListCount())
        {
            if (moveAnswer.CompareMoveList(moveCase))
            {
                DesaEventManager.Instance.Correct();
            }
        }

        for (int i = 0; i < moveAnswer.GetMoveListCount(); i++)
        {
            if (moveCase.GetMoveType(i) != moveAnswer.GetMoveType(i))
            {
                DesaEventManager.Instance.Wrong();
                break;
            }
        }
    }

    private void DesaEventManager_OnCorrect()
    {
        StopCoroutine(timerCoroutine);
        if (showTimeUpCountdownCoroutine != null)
        {
            StopCoroutine(showTimeUpCountdownCoroutine);
            DesaUIManager.Instance.HideTimeUpCountdown();
        }

        AudioManager.Instance.PlayDesaSFX("Correct");

        DesaUIManager.Instance.DisableButton();

        DesaUIManager.Instance.HideMoveIndicatorContainer();

        beriAnimator.SetBool("isIdle", true);

        StartCoroutine(DesaUIManager.Instance.ShowState(DStateType.CORRECT, () =>
        {
            StartCoroutine(PlayAnswer());
        }));

        score += designLevelList[currentLevel].score;
        DesaUIManager.Instance.UpdateScore(score);
    }

    private void DesaEventManager_OnWrong()
    {
        AudioManager.Instance.PlayDesaSFX("Wrong");

        DesaUIManager.Instance.DisableButton();

        DesaInGame.Instance.ZoomToDefault();
        DesaUIManager.Instance.RemoveAllMoveIndicator();
        DesaUIManager.Instance.HideMoveIndicatorContainer();

        health--;
        DesaUIManager.Instance.UpdateHealth(health);

        StartCoroutine(DesaUIManager.Instance.ShowState(DStateType.WRONG, () =>
        {
            if (health > 0)
                DesaEventManager.Instance.PhaseStart();
            else
                CalculateResult();
        }));
    }

    private void MakeCase()
    {
        for (int i = 0; i < designLevelList.Count; i++)
        {
            if (designLevelList[i].onCase == currentCase)
            {
                currentLevel = i;
            }
        }

        moveCase.CreateMoveCase(designLevelList[currentLevel].totalMove, designLevelList[currentLevel].caseType);
    }

    private IEnumerator PlayPhase()
    {
        DesaInGame.Instance.ZoomToChikoKetti();
        yield return new WaitForSeconds(animationTime);

        DesaUIManager.Instance.ShowMoveIndicatorContainer();

        for (int i = 0; i < moveCase.GetMoveListCount(); i++)
        {
            DesaUIManager.Instance.AddActiveMoveIndicator(moveCase.GetMoveType(i));
            PlayMoveAnimation(kettiAnimator, moveCase.GetMoveType(i));
            PlayMoveAnimation(chikoAnimator, moveCase.GetMoveType(i));
            yield return new WaitForSeconds(chikoAnimator.GetCurrentAnimatorStateInfo(0).length);
            StopMoveAnimation(kettiAnimator, moveCase.GetMoveType(i));
            StopMoveAnimation(chikoAnimator, moveCase.GetMoveType(i));

            yield return new WaitForSeconds(animationDelay);
        }

        yield return new WaitForSeconds(animationTime);

        DesaInGame.Instance.ZoomToDefault();
        DesaUIManager.Instance.RemoveAllMoveIndicator();
        DesaUIManager.Instance.HideMoveIndicatorContainer();

        StartCoroutine(DesaUIManager.Instance.ShowState(DStateType.ANSWER, () =>
        {
            DesaUIManager.Instance.EnableButton();
            DesaInGame.Instance.ZoomToBeri();
            DesaUIManager.Instance.ShowMoveIndicatorContainer();

            timerCoroutine = AnswerTimer();
            StartCoroutine(timerCoroutine);
        }));
    }

    private IEnumerator PlayAnswer()
    {
        DesaUIManager.Instance.ShowMoveIndicatorContainer();

        for (int i = 0; i < moveAnswer.GetMoveListCount(); i++)
        {
            DesaUIManager.Instance.ChangeToActiveMoveIndicator(i, moveAnswer.GetMoveType(i));
            PlayMoveAnimation(beriAnimator, moveAnswer.GetMoveType(i));
            yield return new WaitForSeconds(beriAnimator.GetCurrentAnimatorStateInfo(0).length);
            StopMoveAnimation(beriAnimator, moveAnswer.GetMoveType(i));

            yield return new WaitForSeconds(animationDelay);
        }

        beriAnimator.SetBool("isIdle", false);
        yield return new WaitForSeconds(animationTime);

        DesaInGame.Instance.ZoomToDefault();
        DesaUIManager.Instance.RemoveAllMoveIndicator();
        DesaUIManager.Instance.HideMoveIndicatorContainer();

        currentCase++;
        DesaEventManager.Instance.PhaseStart();

    }

    private IEnumerator AnswerTimer()
    {
        yield return new WaitForSeconds(designLevelList[currentLevel].answerTime - 3);

        showTimeUpCountdownCoroutine = DesaUIManager.Instance.ShowTimeUpCountdown(() =>
        {
            DesaEventManager.Instance.Wrong();
        });

        StartCoroutine(showTimeUpCountdownCoroutine);
    }

    private void PlayMoveAnimation(Animator _animator, DMoveType _moveType)
    {
        string moveTypeString = _moveType.ToString();

        _animator.SetBool(moveTypeString, true);
    }

    private void StopMoveAnimation(Animator _animator, DMoveType _moveType)
    {
        string moveTypeString = _moveType.ToString();

        _animator.SetBool(moveTypeString, false);
    }

    private void CalculateResult()
    {
        // 5 score = 1 coin
        int coin = score / 5;

        DesaEventManager.Instance.GameOver(score, coin);
    }
}

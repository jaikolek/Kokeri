using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseState : MonoBehaviour
{
    [Header("Game Info")]
    [SerializeField] private Image infoImage;

    [Header("Countdown")]
    [SerializeField] private int countdownTimer;
    [SerializeField] private List<Sprite> countdownSprite;

    [Header("State")]
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite yourTurnSprite;

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

        StartCoroutine(PhaseStateText(startSprite));
    }

    public IEnumerator PhaseStateText(Sprite _sprite)
    {
        if (!infoImage.gameObject.activeSelf)
            infoImage.gameObject.SetActive(true);

        SetInfoImage(_sprite);

        yield return new WaitForSeconds(1f);

        infoImage.gameObject.SetActive(false);
    }
}

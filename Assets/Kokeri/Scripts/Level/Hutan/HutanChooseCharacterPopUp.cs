using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HutanChooseCharacterPopUp : MonoBehaviour
{
    [SerializeField] private Button chikoButton;
    [SerializeField] private Button kettiButton;
    [SerializeField] private Button beriButton;

    private void Start()
    {
        chikoButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click2");
            HutanEventManager.Instance.CharacterChanged(Character.CHIKO);
        });
        kettiButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click2");
            HutanEventManager.Instance.CharacterChanged(Character.KETTI);
        });
        beriButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click2");
            HutanEventManager.Instance.CharacterChanged(Character.BERI);
        });
    }
}

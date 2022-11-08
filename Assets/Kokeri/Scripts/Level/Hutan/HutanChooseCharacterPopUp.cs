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
        chikoButton.onClick.AddListener(() => HutanEventManager.Instance.CharacterChanged(Character.Chiko));
        kettiButton.onClick.AddListener(() => HutanEventManager.Instance.CharacterChanged(Character.Ketti));
        beriButton.onClick.AddListener(() => HutanEventManager.Instance.CharacterChanged(Character.Beri));
    }
}

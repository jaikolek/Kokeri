using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePopUp : MonoBehaviour
{
    [SerializeField] private Button closeBtn;

    public void Start()
    {
        closeBtn.onClick.AddListener(OnClickClose);
    }

    public virtual void OnClickClose()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.PlaySFX1("Click2");
    }
}

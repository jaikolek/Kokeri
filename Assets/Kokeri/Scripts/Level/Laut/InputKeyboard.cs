using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputKeyboard : MonoBehaviour
{
    public TextUI textUI;
    public TMP_InputField inputField;

    string nama = "asdsd";

    public void Ketik(string name)
    {
        nama = inputField.text;
        Debug.Log(nama);
    }

    public void Submit()
    {
        textUI.namaPenampung = nama;
        textUI.gameOverPanelisActive = false;
    }
}

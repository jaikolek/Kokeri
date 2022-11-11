using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputKeyboard : MonoBehaviour
{

    string nama = "asdsd";

    public void Ketik(string name)
    {
        nama = name;
        Debug.Log(nama);
    }
}

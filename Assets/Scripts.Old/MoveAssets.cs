using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAssets : MonoBehaviour
{
    public static MoveAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite rightSprite;
    public Sprite leftSprite;
    public Sprite upSprite;
    public Sprite downSprite;
}

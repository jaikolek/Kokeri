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

    private MoveInventory MoveCase;
    private MoveInventory MoveAnswer;

    public void HandlePause()
    {
        Time.timeScale = 0f;
    }

    public void HandleUpInput()
    {
        MoveAnswer.AddMove(new Move { moveType = MoveType.UP, value = 3 });
    }

    public void HandleDownInput()
    {
        MoveAnswer.AddMove(new Move { moveType = MoveType.DOWN, value = 4 });
    }

    public void HandleLeftInput()
    {
        MoveAnswer.AddMove(new Move { moveType = MoveType.LEFT, value = 1 });
    }

    public void HandleRightInput()
    {
        MoveAnswer.AddMove(new Move { moveType = MoveType.RIGHT, value = 2 });
    }
}

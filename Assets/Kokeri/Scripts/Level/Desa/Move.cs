using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MoveType moveType;
    public int value;

    public int GetValue()
    {
        return value;
    }

    public MoveType GetMoveType()
    {
        return moveType;
    }

    // public Sprite GetSprite()
    // {
    //     switch (moveType)
    //     { 
    //         default:
    //         case MoveType.RIGHT:     return MoveAssets.Instance.rightSprite;
    //         case MoveType.LEFT:      return MoveAssets.Instance.leftSprite;
    //         case MoveType.UP:        return MoveAssets.Instance.upSprite;
    //         case MoveType.DOWN:      return MoveAssets.Instance.downSprite;
    //     }
    // }
}

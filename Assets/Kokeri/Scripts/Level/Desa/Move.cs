using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MoveType moveType;

    public Move(MoveType _moveType)
    {
        this.moveType = _moveType;
    }

    public MoveType GetMoveType()
    {
        return moveType;
    }
}

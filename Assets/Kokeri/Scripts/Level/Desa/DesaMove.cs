using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaMove
{
    public DMoveType moveType;

    public DesaMove(DMoveType _moveType)
    {
        this.moveType = _moveType;
    }

    public DMoveType GetMoveType()
    {
        return moveType;
    }
}

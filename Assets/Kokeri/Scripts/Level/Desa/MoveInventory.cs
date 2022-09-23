using System;
using System.Collections;
using System.Collections.Generic;

public class MoveInventory
{
    public event EventHandler OnItemListChanged;
    private List<Move> moveList;

    public MoveInventory()
    {
        moveList = new List<Move>();
    }

    public void AddMove(Move _move)
    {
        moveList.Add(_move);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveMove()
    {
        moveList.Clear();
    }

    public List<Move> GetMoveList()
    {
        return moveList;
    }

    public int GetMoveListCount()
    {
        return Convert.ToInt32(moveList.Count);
    }

    public int GetMoveValue(int _index)
    {
        return moveList[_index].GetValue();
    }
}

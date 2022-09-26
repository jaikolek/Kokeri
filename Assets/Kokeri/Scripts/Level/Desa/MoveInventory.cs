using System;
using System.Collections;
using System.Collections.Generic;

public class MoveInventory
{
    // public event EventHandler OnItemListChanged;
    private List<Move> moveList;

    public MoveInventory()
    {
        moveList = new List<Move>();
    }

    public void AddMove(Move _move)
    {
        moveList.Add(_move);
        // OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveMove(Move _move)
    {
        moveList.Remove(_move);
        // OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ClearMoveList()
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

    public MoveType GetMoveType(int _index)
    {
        return moveList[_index].GetMoveType();
    }
    // ====================================================================================================


    // ====================================================================================================
    public void CreateMoveCase(int _totalCase, CaseType _caseType)
    {
        ClearMoveList();

        for (int i = 0; i < _totalCase; i++)
        {
            int randomValue;

            switch (_caseType)
            {
                case CaseType.HORIZONTAL:
                    randomValue = UnityEngine.Random.Range(3, 4 + 1);
                    break;
                case CaseType.VERTICAL:
                    randomValue = UnityEngine.Random.Range(1, 2 + 1);
                    break;
                case CaseType.ALL:
                    randomValue = UnityEngine.Random.Range(1, 4 + 1);
                    break;
                default:
                    randomValue = 0;
                    break;
            }

            switch (randomValue)
            {
                case 1:
                    AddMove(new Move(MoveType.UP));
                    break;
                case 2:
                    AddMove(new Move(MoveType.DOWN));
                    break;
                case 3:
                    AddMove(new Move(MoveType.LEFT));
                    break;
                case 4:
                    AddMove(new Move(MoveType.RIGHT));
                    break;
            }
        }
    }
}

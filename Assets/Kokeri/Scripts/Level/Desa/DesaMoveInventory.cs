using System;
using System.Collections;
using System.Collections.Generic;

public class DesaMoveInventory
{
    private List<DesaMove> moveList;

    public DesaMoveInventory()
    {
        moveList = new List<DesaMove>();
    }

    public void AddMove(DesaMove _move)
    {
        moveList.Add(_move);
    }

    public void ClearMoveList()
    {
        moveList.Clear();
    }

    public List<DesaMove> GetMoveList()
    {
        return moveList;
    }

    public int GetMoveListCount()
    {
        return Convert.ToInt32(moveList.Count);
    }

    public DMoveType GetMoveType(int _index)
    {
        return moveList[_index].GetMoveType();
    }

    public bool CompareMoveList(DesaMoveInventory _moveInventory)
    {
        if (moveList.Count != _moveInventory.GetMoveListCount())
        {
            return false;
        }

        for (int i = 0; i < moveList.Count; i++)
        {
            if (moveList[i].GetMoveType() != _moveInventory.GetMoveType(i))
            {
                return false;
            }
        }

        return true;
    }
    // ====================================================================================================


    // ====================================================================================================
    public void CreateMoveCase(int _totalCase, DCaseType _caseType)
    {
        ClearMoveList();

        for (int i = 0; i < _totalCase; i++)
        {
            int randomValue;

            switch (_caseType)
            {
                case DCaseType.HORIZONTAL:
                    randomValue = UnityEngine.Random.Range(3, 4 + 1);
                    break;
                case DCaseType.VERTICAL:
                    randomValue = UnityEngine.Random.Range(1, 2 + 1);
                    break;
                case DCaseType.ALL:
                    randomValue = UnityEngine.Random.Range(1, 4 + 1);
                    break;
                default:
                    randomValue = 0;
                    break;
            }

            switch (randomValue)
            {
                case 1:
                    AddMove(new DesaMove(DMoveType.UP));
                    break;
                case 2:
                    AddMove(new DesaMove(DMoveType.DOWN));
                    break;
                case 3:
                    AddMove(new DesaMove(DMoveType.LEFT));
                    break;
                case 4:
                    AddMove(new DesaMove(DMoveType.RIGHT));
                    break;
            }
        }
    }
}

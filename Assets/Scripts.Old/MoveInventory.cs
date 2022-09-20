// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MoveInventory
// {
//     public event EventHandler OnItemListChanged;
//     private List<Move> moveList;

//     public MoveInventory()
//     {
//         moveList = new List<Move>();
//     }

//     public void AddMove(Move move)
//     {
//         moveList.Add(move);
//         OnItemListChanged?.Invoke(this, EventArgs.Empty);
//         //Debug.Log("Add: " + moveList.Count);
//     }

//     public void RemoveMove()
//     {
//         moveList.Clear();
//         //Debug.Log("movelist: " + moveList.Count);
//     }

//     public List<Move> GetMoveList()
//     {
//         return moveList;
//     }

//     public int getCount()
//     {
//         return Convert.ToInt32(moveList.Count);
//     }

//     public ArrayList getValue()
//     {
//         ArrayList value = new ArrayList();
//         for (int i = 0; i < moveList.Count; i++)
//         {
//             //value.Add(moveList[i].value);
//             value[i] = moveList[i].value;
//         }

//         //  reguler
//         //int count = moveList.Count;
//         //int[] value = new int[count];
//         //for (int i = 0; i < count; i++)
//         //{
//         //    value[i] = moveList[i].getValue();
//         //}

//         return value;
//     }

//     public List<Move> getList()
//     {
//         List<Move> value = new List<Move>();
//         foreach(Move move in moveList)
//         {
//             value.Add(move);
//         }

//         Debug.Log("GetList Count: " + value.Count);
//         return value;
//     }
// }

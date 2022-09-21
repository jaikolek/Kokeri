// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Move
// {
//     public enum MoveType
//     { 
//         Right,
//         Left,
//         Up,
//         Down,
//     }

//     public MoveType moveType;
//     public int value;

//     public int getValue()
//     {
//         return value;
//     }

//     public MoveType getMoveType()
//     {
//         return moveType;
//     }

//     public Sprite GetSprite()
//     {
//         switch (moveType)
//         { 
//             default:
//             case MoveType.Right:     return MoveAssets.Instance.rightSprite;
//             case MoveType.Left:      return MoveAssets.Instance.leftSprite;
//             case MoveType.Up:        return MoveAssets.Instance.upSprite;
//             case MoveType.Down:      return MoveAssets.Instance.downSprite;
//         }
//     }
// }

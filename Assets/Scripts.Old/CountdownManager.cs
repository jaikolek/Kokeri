// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class CountdownManager : MonoBehaviour
// {
//     //  countdown
//     public int Count_Down_Time;
//     public Text Count_Down_Display;

//     //  countdown script
//     public IEnumerator Count_down_start()
//     {
//         while (Count_Down_Time > 0)
//         {
//             Count_Down_Display.text = Count_Down_Time.ToString();

//             yield return new WaitForSeconds(1f);

//             Count_Down_Time--;
//         }

//         Count_Down_Display.text = "START!";

//         yield return new WaitForSeconds(1f);
//         Count_Down_Display.gameObject.SetActive(false);
//     }
// }

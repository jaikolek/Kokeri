// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FunctionTimer
// {
//     private static List<FunctionTimer> Active_Timer_List;
//     private static GameObject Init_Game_Object;

//     private static void Init_if_needed()
//     {
//         if (Init_Game_Object == null)
//         {
//             Init_Game_Object = new GameObject("FuntionTimer_InitGameObject");
//             Active_Timer_List = new List<FunctionTimer>();
//         }
//     }

//     public static FunctionTimer Create(Action Action_Executed, float Timer, string Timer_Name = null)
//     {
//         Init_if_needed();
//         GameObject Game_Object = new GameObject("FunctionTimer", typeof(MonoBehaviorHook));
//         FunctionTimer Function = new FunctionTimer(Action_Executed, Timer, Timer_Name, Game_Object);
        
//         Game_Object.GetComponent<MonoBehaviorHook>().On_Update = Function.Update;

//         Active_Timer_List.Add(Function);
//         return Function;
//     }

//     private static void Remove_timer(FunctionTimer Function)
//     {
//         Init_if_needed();
//         Active_Timer_List.Remove(Function);
//     }

//     public static void Stop_timer(string Timer_Name)
//     {
//         for (int i = 0; i < Active_Timer_List.Count; i++)
//         {
//             if (Active_Timer_List[i].Timer_Name == Timer_Name)
//             {
//                 //  stop this timer
//                 Active_Timer_List[i].Destroy_it_self();
//                 i--;
//             }
//         }
//     }

//     //  Dummy class to have access to Monobehavior Function
//     private class MonoBehaviorHook : MonoBehaviour
//     {
//         public Action On_Update;
//         private void Update()
//         {
//             if (On_Update != null) On_Update();
//         }
//     }

//     private Action Action_Executed;
//     private float Timer;
//     private string Timer_Name;
//     private GameObject Game_Object;
//     private bool Is_Destroyed;

//     private FunctionTimer(Action Action_Executed, float Timer, string Timer_Name, GameObject Game_Object)
//     {
//         this.Action_Executed = Action_Executed;
//         this.Timer = Timer;
//         this.Game_Object = Game_Object;
//         this.Timer_Name = Timer_Name;
//         Is_Destroyed = false;
//     }

//     public void Update()
//     {
//         if (!Is_Destroyed)
//         {
//             Timer -= Time.deltaTime;
//             if (Timer < 0)
//             {
//                 //  trigger run
//                 Action_Executed();
//                 Destroy_it_self();
//             }
//         }
//     }

//     private void Destroy_it_self()
//     {
//         Is_Destroyed = true;
//         UnityEngine.Object.Destroy(Game_Object);
//         Remove_timer(this);
//     }
// }

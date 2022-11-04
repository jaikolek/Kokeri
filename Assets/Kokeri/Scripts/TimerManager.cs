using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class TimerManager : MonoBehaviour
// {
//     #region singleton
//     private static TimerManager instance;
//     public static TimerManager Instance
//     {
//         get
//         {
//             if (instance == null)
//             {
//                 instance = FindObjectOfType<TimerManager>();
//                 if (instance == null)
//                 {
//                     GameObject obj = new GameObject();
//                     obj.name = typeof(TimerManager).Name;
//                     instance = obj.AddComponent<TimerManager>();
//                 }
//             }
//             return instance;
//         }
//     }

//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(this.gameObject);
//         }
//         else
//         {
//             Destroy(this.gameObject);
//         }
//     }
//     #endregion singleton
//     // ====================================================================================================


//     // ====================================================================================================
//     public Timer CreateTimer(float duration, bool isLooping)
//     {
//         Timer timer = new Timer(duration, isLooping);
//         return timer;
//     }
// }

// public class Timer : MonoBehaviour
// {
//     public float duration;
//     public bool isLooping;
//     public bool isRunning;
//     public bool isPaused;
//     public float timeLeft;

//     public Timer(float duration, bool isLooping)
//     {
//         this.duration = duration;
//         this.isLooping = isLooping;
//         this.isRunning = false;
//         this.isPaused = false;
//         this.timeLeft = duration;
//     }

//     public void StartTimer()
//     {
//         isRunning = true;
//         isPaused = false;
//     }

//     public void PauseTimer()
//     {
//         isRunning = false;
//         isPaused = true;
//     }

//     public void StopTimer()
//     {
//         isRunning = false;
//         isPaused = false;
//         timeLeft = duration;
//     }

//     public void UpdateTimer()
//     {
//         if (isRunning)
//         {
//             timeLeft -= Time.deltaTime;
//             if (timeLeft <= 0)
//             {
//                 if (isLooping)
//                 {
//                     timeLeft = duration;
//                 }
//                 else
//                 {
//                     StopTimer();
//                 }
//             }
//         }
//     }
// }

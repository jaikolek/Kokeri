// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class GamePauseUI : MonoBehaviour
// {
//     [SerializeField] private GameObject pauseGameObject;
//     public AudioSource sfxButton;

//     public void Continue_Button_Clicked()
//     {
//         sfxButton.PlayOneShot(sfxButton.clip);
//         pauseGameObject.SetActive(false);
//         Time.timeScale = 1f;
//     }

//     public void Level_Button_Clicked()
//     {
//         sfxButton.PlayOneShot(sfxButton.clip);
//         SceneManager.LoadScene(1);
//     }

//     public void Home_Button_Clicked()
//     {
//         sfxButton.PlayOneShot(sfxButton.clip);
//         SceneManager.LoadScene(0);
//     }
// }

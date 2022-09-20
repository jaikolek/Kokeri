// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Video;
// using UnityEngine.SceneManagement;

// public class LevelManager : MonoBehaviour
// {
//     //  Game Object
//     public GameObject Main;
//     public GameObject Shop;
//     public GameObject Shop_Chiko;
//     public GameObject Shop_Ketti;
//     public GameObject Shop_Beri;
//     public GameObject Coming_Soon_Hutan;
//     public GameObject Coming_Soon_Laut;

//     //  Audio
//     public AudioSource Button_Sound_1;
//     public AudioSource Button_Sound_2;
//     public AudioSource Main_Menu_Sound_1;

//     //  Video Prolog
//     [SerializeField] private VideoPlayer video;
//     [SerializeField] private GameObject videoUI;
 
//     void Start()
//     {
//         Time.timeScale = 1f;

//         Coming_Soon_Hutan.SetActive(false);
//         Coming_Soon_Laut.SetActive(false);
//         videoUI.SetActive(false);
//         Main.SetActive(true);
//         Shop.SetActive(false);
//         Shop_Chiko.SetActive(false);
//         Shop_Ketti.SetActive(false);
//         Shop_Beri.SetActive(false);
//         Main_Menu_Sound_1.Play();
//     }

//     private void Update()
//     {
//         Main_Menu_Sound_1.volume = MenuManager.Music_Volume;
//         Button_Sound_1.volume = MenuManager.Sfx_Volume;
//         Button_Sound_2.volume = MenuManager.Sfx_Volume;
//     }

//     // UI BUtton
//     public void Back_button_clicked()
//     {
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         SceneManager.LoadScene(0);
//     }

//     public void Desa_button_clicked()
//     {
//         Button_Sound_1.PlayOneShot(Button_Sound_1.clip);
//         StartCoroutine(videoFinished());
//     }

//     public void Laut_button_clicked()
//     {
//         Button_Sound_1.PlayOneShot(Button_Sound_2.clip);
//     }

//     public void Hutan_button_clicked()
//     {
//         Button_Sound_1.PlayOneShot(Button_Sound_2.clip);
//     }

//     public void Shop_button_clicked()
//     {
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         Main.SetActive(false);
//         Shop.SetActive(true);
//         Shop_Chiko.SetActive(true);
//         Shop_Ketti.SetActive(false);
//         Shop_Beri.SetActive(false);
//     }

//     public void Shop_chiko_button_clicked()
//     {
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         Main.SetActive(false);
//         Shop.SetActive(true);
//         Shop_Ketti.SetActive(false);
//         Shop_Beri.SetActive(false);
//         Shop_Chiko.SetActive(true);
//     }

//     public void Shop_ketty_button_clicked()
//     {
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         Main.SetActive(false);
//         Shop.SetActive(true);
//         Shop_Chiko.SetActive(false);
//         Shop_Beri.SetActive(false);
//         Shop_Ketti.SetActive(true);
//     }

//     public void Shop_beri_button_clicked()
//     {
//         Main.SetActive(false);
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         Shop.SetActive(true);
//         Shop_Chiko.SetActive(false);
//         Shop_Ketti.SetActive(false);
//         Shop_Beri.SetActive(true);
//     }

//     public void Exit_shop_button_clicked()
//     {
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         Main.SetActive(true);
//         Shop.SetActive(false);
//         Shop_Chiko.SetActive(false);
//         Shop_Ketti.SetActive(false);
//         Shop_Beri.SetActive(false);
//     }

//     public void Coming_soon_hutan_button_clicked()
//     {
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         Coming_Soon_Hutan.SetActive(true);
//     }

//     public void Back_on_Coming_soon_hutan_button_clicked()
//     {
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         Coming_Soon_Hutan.SetActive(false);
//     }

//     public void Coming_soon_laut_button_clicked()
//     {
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         Coming_Soon_Laut.SetActive(true);
//     }

//     public void Back_on_Coming_soon_laut_button_clicked()
//     {
//         Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
//         Coming_Soon_Laut.SetActive(false);
//     }

//     IEnumerator videoFinished()
//     {
//         Main_Menu_Sound_1.Pause();
//         videoUI.SetActive(true);
//         video.Play();
//         yield return new WaitForSeconds((float)video.length);
//         videoUI.SetActive(false);
//         SceneManager.LoadScene(2);
//     }
// }

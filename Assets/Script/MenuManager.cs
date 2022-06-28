using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Game object
    public GameObject Menu_Panel_Main;
    public GameObject Menu_Panel_Setting;
    public GameObject Menu_Panel_Ranking_1;
    public GameObject Menu_Panel_Ranking_2;
    public GameObject Shop;
    public GameObject Shop_Chiko;
    public GameObject Shop_Ketti;
    public GameObject Shop_Beri;

    // sound
    public AudioSource Button_Sound_1;
    public AudioSource Button_Sound_2;
    public AudioSource Main_Menu_Sound_1;
    public static float Music_Volume = 1f;
    public static float Sfx_Volume = 1f;

    [SerializeField] private  Slider SFX_Slider;
    [SerializeField] private Slider Backsound_Slider;

    // data variable
    // float currentTime = 0f;

    void Start()
    {
        Time.timeScale = 1f;
        Menu_Panel_Main.SetActive(true);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(false);
        Shop_Chiko.SetActive(false);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);

        Main_Menu_Sound_1.Play();

        SFX_Slider.value = PlayerPrefs.GetFloat("SFX_Volume", Sfx_Volume);
        Backsound_Slider.value = PlayerPrefs.GetFloat("Backsound_Volume", Music_Volume);
    }

    void Update()
    {
        Main_Menu_Sound_1.volume = Music_Volume;
        Button_Sound_1.volume = Sfx_Volume;
        Button_Sound_2.volume = Sfx_Volume;

        // timer
        // currentTime += 1 * Time.deltaTime;
        // print(currentTime);
    }

    // Volume setting
    public void Update_volume()
    {
        Music_Volume = Backsound_Slider.value;
        Sfx_Volume = SFX_Slider.value;
        PlayerPrefs.GetFloat("Backsound_Volume", Backsound_Slider.value);
        PlayerPrefs.GetFloat("SFX_Volume", SFX_Slider.value);
    }

    // Button
    public void Start_button_clicked()
    {
        Button_Sound_1.PlayOneShot(Button_Sound_1.clip);
        Menu_Panel_Main.SetActive(true);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(false);
        Shop_Chiko.SetActive(false);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);

        //if (currentTime > 2f)
        SceneManager.LoadScene(1);
    }

    public void Setting_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(false);
        Menu_Panel_Setting.SetActive(true);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(false);
        Shop_Chiko.SetActive(false);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);
    }

    public void Ranking_1_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(true);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(false);
        Shop_Chiko.SetActive(false);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);
    }

    public void Ranking_2_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(true);
        Shop.SetActive(false);
        Shop_Chiko.SetActive(false);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);
    }

    public void Back_1_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(true);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(false);
        Shop_Chiko.SetActive(false);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);
    }

    public void Back_2_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(false);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(true);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(false);
        Shop_Chiko.SetActive(false);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);
    }

    // Shop
    public void Shop_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(false);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(true);
        Shop_Chiko.SetActive(true);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);
    }

    public void Shop_chiko_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(false);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(true);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);
        Shop_Chiko.SetActive(true);
    }

    public void Shop_ketty_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(false);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(true);
        Shop_Chiko.SetActive(false);
        Shop_Beri.SetActive(false);
        Shop_Ketti.SetActive(true);
    }

    public void Shop_beri_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(false);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(true);
        Shop_Chiko.SetActive(false);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(true);
    }

    public void Exit_shop_button_clicked()
    {
        Button_Sound_2.PlayOneShot(Button_Sound_2.clip);
        Menu_Panel_Main.SetActive(true);
        Menu_Panel_Setting.SetActive(false);
        Menu_Panel_Ranking_1.SetActive(false);
        Menu_Panel_Ranking_2.SetActive(false);
        Shop.SetActive(false);
        Shop_Chiko.SetActive(false);
        Shop_Ketti.SetActive(false);
        Shop_Beri.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUI : MonoBehaviour
{    
    public TextMeshProUGUI score;
    public TextMeshProUGUI nyawa;
    public TextMeshProUGUI koinUI;
    public TextMeshProUGUI ikanUI;
    public TextMeshProUGUI ikanUIGameOver;

    public TextMeshProUGUI nama;
    public TextMeshProUGUI scoreBoard;

    public string namaPenampung;

    public Image[] iconNyawa;
    public GameObject gameOverPanel;

    public int TotalSkor, convertScore, koin, nyawaPlayer = 3, minNyawa;
    public int ikanCounter;

    // Start is called before the first frame update
    void Start()
    {
        
        koin = 0;
        TotalSkor = 0;
        nyawaPlayer = 3;
        ikanCounter = 0;
        for(int i = 0; i < iconNyawa.Length; i++)
        {
            iconNyawa[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        minNyawa = nyawaPlayer;
        ikanUI.text = ikanCounter.ToString();
        nyawa.text = "X" + nyawaPlayer.ToString();

        // untuk game over
        score.text = TotalSkor.ToString();
        koinUI.text = koin.ToString();
        ikanUIGameOver.text = ikanCounter.ToString();

        // Leader Board
        nama.text = "1. " + namaPenampung + " " + TotalSkor.ToString();
        

        if(nyawaPlayer <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        
    }


}

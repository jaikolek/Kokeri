using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUI : MonoBehaviour
{    
    public TextMeshProUGUI score;
    public TextMeshProUGUI nyawa;
    public TextMeshProUGUI score2;
    public TextMeshProUGUI koinUI;

    public Image[] iconNyawa;
    public GameObject gameOverPanel;

    public int TotalSkor, convertScore, koin, nyawaPlayer = 3, minNyawa;

    // Start is called before the first frame update
    void Start()
    {
        koin = 0;
        TotalSkor = 0;
        nyawaPlayer = 3;
        for(int i = 0; i < iconNyawa.Length; i++)
        {
            iconNyawa[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        minNyawa = nyawaPlayer;
        score.text = "Skor : " + TotalSkor.ToString();
        nyawa.text = "X" + nyawaPlayer.ToString();


        if(nyawaPlayer <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            score2.text = TotalSkor.ToString();
            koinUI.text = koin.ToString();
        }

    }
}

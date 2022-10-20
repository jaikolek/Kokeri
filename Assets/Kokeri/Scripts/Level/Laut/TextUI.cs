using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public Text score;
    public Text coin;
    public Text nyawa;
    public int TotalSkor, convertScore, koin, nyawaPlayer = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        coin.text = "Coin   : " + koin.ToString();
        score.text = "Score : " + TotalSkor.ToString();
        nyawa.text = "Nyawa : " + nyawaPlayer.ToString();
    }
}

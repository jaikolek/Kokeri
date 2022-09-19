using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerDesa_1 : MonoBehaviour
{
    //  MOTE FOR PROGRAMMER
    //  Function example
    //  Time.timeScale = 0f; = stop time in game

    //  Timer function with same timer
    //  FunctionTimer.Create(test, 2f, "timer1");
    //  FunctionTimer.Create(test2, 4f, "timer2");
    //  FunctionTimer.Stop_timer("timer1");

    //  call two function with threading delay
    //  Delay.Delaying(test, test2, 2f);


    //  example for code addMove
    //Move_Inventory.AddMove(new Move { moveType = Move.MoveType.Right, value = 1 });
    //Move_Inventory.AddMove(new Move { moveType = Move.MoveType.Left, value = 2 });
    //Move_Inventory.AddMove(new Move { moveType = Move.MoveType.Up, value = 3 });
    //Move_Inventory.AddMove(new Move { moveType = Move.MoveType.Down, value = 4 });
    //  Right, Lift, Up, Down

    //  UI
    [SerializeField] private GameObject Pause;

    //  Audio
    public AudioSource backsound;
    public AudioSource sfxButton;

    //  count down
    public CountdownManager Count_Down;

    //  inventory
    private MoveInventory Move_Inventory;
    private MoveInventory Player_Input;
    [SerializeField] private MoveUI Move_UI;

    //  game object
    [SerializeField] GameObject playerInput;

    //  Animation
    [SerializeField] private Animator Beri;
    [SerializeField] private Animator Chiko;
    [SerializeField] private Animator Keti;

    //  game
    public const int Right = 1;
    public const int Left = 2;
    public const int Up = 3;
    public const int Down = 4;

    public int timer = 4;
    public int input = 1;
    private int randValue = 4;

    private int score = 0;
    private int chance = 3;

    private bool playAgain = true;

    //  Game Info
    [SerializeField] private Text Score;

    private void Awake()
    {   
        Move_Inventory = new MoveInventory();
        Player_Input = new MoveInventory();
    }

    void Start()
    {
        Time.timeScale = 1f;

        //  Sound
        backsound.volume = MenuManager.Music_Volume;
        sfxButton.volume = MenuManager.Sfx_Volume;
        backsound.Play();

        //  UI Start
        Pause.SetActive(false);
        playerInput.SetActive(false);

        //  Game Start
        StartCoroutine(Count_Down.Count_down_start());

        //StartCoroutine(Fase_One());
    }

    void Update()
    {
        if(playAgain)
        {
            StartCoroutine(Fase());
        } 
        
        if (chance < 0)
        {
            Time.timeScale = 0f;
            //  you loose
        }

        Score.text = "Score: " + score;
    }

    //  fase engine
    IEnumerator Fase()
    {
        //  fase 1
        playAgain = false;
        int[] random = new int[randValue];
        Beri.SetFloat("Idle", 1);

        yield return new WaitForSeconds((float)timer);
        for (int i = 0; i < randValue; i++)
        {
            random[i] = Randomming(4);

            switch (random[i])
            {
                case 1:
                    Move_Inventory.AddMove(new Move { moveType = Move.MoveType.Right, value = 1 });
                    break;
                    
                case 2:
                    Move_Inventory.AddMove(new Move { moveType = Move.MoveType.Left, value = 2 });
                    break;

                case 3:
                    Move_Inventory.AddMove(new Move { moveType = Move.MoveType.Up, value = 3 });
                    break;

                case 4:
                    Move_Inventory.AddMove(new Move { moveType = Move.MoveType.Down, value = 4 });
                    break;

                default: randValue++;
                    break;
            }
            Move_UI.SetMoveInventory(Move_Inventory);
            yield return new WaitForSeconds(timer - 3f);
            Debug.Log("Move: " + random[i]);
        }

        for (int i = 0; i < Move_Inventory.getCount(); i++)
        {
            Chiko.SetBool("Move", true);
            Chiko.SetFloat("Direction", (float)random[i]);
            Keti.SetBool("Move", true);
            Keti.SetFloat("Direction", (float)random[i]);
            yield return new WaitForSeconds(1f);
            Chiko.SetBool("Move", false);
            Keti.SetBool("Move", false);
        }
        
        yield return new WaitForSeconds((float)(timer + 2));
        List<Move> moveAnswer = Move_Inventory.getList();
        Move_UI.RemoveMove();
        Debug.Log("Remove UI: " + Move_Inventory.getCount());


        //  fase 2
        yield return new WaitForSeconds((float)(timer - 2));
        input = 1;
        Beri.SetFloat("Idle", 2);
        playerInput.SetActive(true);
        yield return new WaitForSeconds((float)(timer + 2));
        playerInput.SetActive(false);

        yield return new WaitForSeconds((float)(timer - 3));
        Beri.SetBool("Move", true);

        //  player input = 0?
        Debug.Log("Player Input Count: " + Player_Input.getCount());
        List<Move> beriMove = Player_Input.getList();
        Debug.Log("input count: " + beriMove.Count);
        for (int i = 0; i < (int)beriMove.Count; i++)
        {
            Debug.Log("Input: " + beriMove[i].value);
            switch (beriMove[i].value)
            {
                case 1:
                    Beri.SetFloat("Direction", 2);
                    break;

                case 2:
                    Beri.SetFloat("Direction", 1);
                    break;

                case 3:
                    Beri.SetFloat("Direction", 3);
                    break;

                case 4:
                    Beri.SetFloat("Direction", 4);
                    break;
            }

            yield return new WaitForSeconds((float)(timer - 3));
        }

        yield return new WaitForSeconds((float)(timer - 2));
        Beri.SetBool("Move", false);
        Beri.SetFloat("Idle", 2);


        //  fase 3
        yield return new WaitForSeconds(4f);
        List<Move> inputPlayer = Player_Input.getList();
        int count = 0;

        for (int i = 0; i < Player_Input.getCount(); i++)
        {
            Debug.Log("Move Answer: " + moveAnswer[i].value);
            Debug.Log("Player Input: " + inputPlayer[i].value);
            if (moveAnswer[i].value == inputPlayer[i].value)
            {
                count++;
            }
        }

        if (count == randValue)
        {
            count--;
            score += 10 / (randValue - count);
        }
        else if (count <= 0)
        {
            Debug.Log("Count Zero");
            score += 0;
        } else
        {
            count--;
            score += 10 / (randValue - count);
        }

        Debug.Log("Score: " + score);

        yield return new WaitForSeconds((float)(timer - 3));
        Move_UI.RemoveMove();
        Move_Inventory.RemoveMove();
        Player_Input.RemoveMove();
        playAgain = true;
    }

    //  randoming
    private int Randomming(int Max_Random)
    {
        int Number = Random.Range(1, Max_Random + 1);

        return Number;
    }

    //  UI Button and else
    public void Pause_Button_Clicked()
    {
        sfxButton.PlayOneShot(sfxButton.clip);
        Pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Right_Button_Clicked()
    {
        if (input <= randValue)
        {
            input++;
            sfxButton.PlayOneShot(sfxButton.clip);
            Player_Input.AddMove(new Move { moveType = Move.MoveType.Right, value = 1 });
            Debug.Log("Move Count: " + Player_Input.getCount());
            Move_UI.SetMoveInventory(Player_Input);
        } 
        else
        {
            playerInput.SetActive(false);
        }
    }

    public void Left_Button_Clicked()
    {
        if (input <= randValue)
        {
            input++;
            sfxButton.PlayOneShot(sfxButton.clip);
            Player_Input.AddMove(new Move { moveType = Move.MoveType.Left, value = 2 });
            Debug.Log("Move Count: " + Player_Input.getCount());
            Move_UI.SetMoveInventory(Player_Input);
        }
        else
        {
            playerInput.SetActive(false);
        }
    }

    public void Up_Button_Clicked()
    {
        if (input <= randValue)
        {
            input++;
            sfxButton.PlayOneShot(sfxButton.clip);
            Player_Input.AddMove(new Move { moveType = Move.MoveType.Up, value = 3 });
            Debug.Log("Move Count: " + Player_Input.getCount());
            Move_UI.SetMoveInventory(Player_Input);
        }
        else
        {
            playerInput.SetActive(false);
        }
    }

    public void Down_Button_Clicked()
    {
        if (input <= randValue)
        {
            input++;
            sfxButton.PlayOneShot(sfxButton.clip);
            Player_Input.AddMove(new Move { moveType = Move.MoveType.Down, value = 4 });
            Debug.Log("Move Count: " + Player_Input.getCount());
            Move_UI.SetMoveInventory(Player_Input);
        }
        else
        {
            playerInput.SetActive(false);
        }
    }
}

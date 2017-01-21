using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
    static  public int moneyPlayer1;
    static public int moneyPlayer2;
    public int moneyTrue;
    static public int staticMoneyForSeaShell;
    public int moneyForSeaShell;

    public int laneSelectedP1 = 2;
    public int laneSelectedP2 = 2;

    public List<GameObject> player1Lanes;
    public List<GameObject> player2Lanes;

    public Image player1LaneSelectorRes;
    public Image player2LaneSelectorRes;

    public WaveManager waveManager;

    public Text money1Text;
    public Text money2Text;

    private string player1Mapping = "";
    private string player2Mapping = "";

    private int player1Pal = -1;
    private int player2Pal = -1;

    private bool down1 = false;
    private bool down2 = false;

    private Image player1LaneSelector;
    private Image player2LaneSelector;

    // Use this for initialization
    void Start () {
        InputManager.moneyPlayer1 = moneyTrue;
        InputManager.moneyPlayer2 = moneyTrue;
        InputManager.staticMoneyForSeaShell = moneyForSeaShell;

        //player1LaneSelector = Instantiate(player1LaneSelectorRes , player1Lanes[laneSelectedP1].transform.position , Quaternion.identity) as Image;
       // player2LaneSelector = Instantiate(player2LaneSelectorRes, player2Lanes[laneSelectedP2].transform.position, Quaternion.identity) as Image;

    }
	
	// Update is called once per frame
	void Update () {

        //si on sait quel gamepad est qui, on y va
        if(player1Mapping != "" && player2Mapping != "")
        {

           // Debug.Log("player 22" + Input.GetAxis("Vertical" + player2Mapping));

          
             if (Input.GetKeyDown((KeyCode)player1Pal))
            {
                waveManager.SendUnit(laneSelectedP1, false);
            }
            else if (Input.GetKeyDown((KeyCode)player2Pal))
            {
                waveManager.SendUnit(laneSelectedP2, true);
            }
            else if (Input.GetAxis("Vertical" + player1Mapping) > 0 && !down1)
            {
                laneSelectedP1 = (laneSelectedP1 + 1) % 3;
               // player1LaneSelector.transform.position = player1Lanes[laneSelectedP1].transform.position;
                down1 = true;
            }
            else if (Input.GetAxis("Vertical" + player1Mapping) < 0 && !down1)
            {
                laneSelectedP1 = (laneSelectedP1 - 1);
               

                if (laneSelectedP1 < 0)
                    laneSelectedP1 = 2;

               // player1LaneSelector.transform.position = player1Lanes[laneSelectedP1].transform.position;

                down1 = true;
            }
            else if (Input.GetAxis("Vertical" + player1Mapping) == 0)
            {
                down1 = false;
            }
             if (Input.GetAxis("Vertical" + player2Mapping) > 0 && !down2)
            {

                laneSelectedP2 = (laneSelectedP2 + 1) % 3;
               // player2LaneSelector.transform.position = player2Lanes[laneSelectedP2].transform.position;

                down2 = true;
            }
            else if (Input.GetAxis("Vertical" + player2Mapping) < 0 && !down2)
            {
                laneSelectedP2 = (laneSelectedP2 - 1);

                if (laneSelectedP2 < 0)
                    laneSelectedP2 = 2;

                // player2LaneSelector.transform.position = player2Lanes[laneSelectedP2].transform.position;

                down2 = true;
            }
            else if (Input.GetAxis("Vertical" + player2Mapping) == 0)
            {
                down2 = false;
            }
        }
        else // demande aux joueurs d'appuyer sur une touche pour savoir qui est qui
        {
             for(int i = 350; i <= 490; i+=20)
            {

                if (Input.GetKeyDown((KeyCode) i))
                {

                    //Debug.Log(i);

                    if (player1Mapping == "")
                    {
                        player1Mapping = "JP" + (((i - 350) / 20) + 1);
                        player1Pal = i;
                    }
                       
                    else
                    {
                        player2Mapping = "JP" + (((i - 350) / 20) + 1);
                        player2Pal = i;
                    }
                        
                }
            }
        }

        money1Text.text = InputManager.moneyPlayer1.ToString();
        money2Text.text = InputManager.moneyPlayer2.ToString();

        Debug.Log("Player 1 :" + player1Mapping);
        Debug.Log("Player 2 :" + player2Mapping);
	}

    static public void addMoney(int _money, int _player)
    {
        if(_player == 0)
        {
            moneyPlayer1 += _money;
        }
        else
        {
            moneyPlayer2 += _money;
        }
    }
}

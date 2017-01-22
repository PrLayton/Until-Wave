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

    public List<GameObject> playerUltiPoints;

    public GameObject player1Cursor;
    public GameObject player2Cursor;

    public WaveManager waveManager;

    public Text money1Text;
    public Text money2Text;

    private string player1Mapping = "";
    private string player2Mapping = "";

    private int player1Pal = -1;
    private int player2Pal = -1;

    private bool down1 = false;
    private bool down2 = false;

    private bool player1IsUlti = false;
    private bool player2IsUlti = false;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    ToolAnimator taLeftUltraWage;
    [SerializeField]
    float timeAnimationL;
    [SerializeField]
    ToolAnimator taRightUltraWage;
    [SerializeField]
    float timeAnimationR;

    // Use this for initialization
    void Start () {
        InputManager.moneyPlayer1 = moneyTrue;
        InputManager.moneyPlayer2 = moneyTrue;
        InputManager.staticMoneyForSeaShell = moneyForSeaShell;

        player1Cursor.GetComponent<CursorHandler>().timeActivated = waveManager.cooldown;
        player2Cursor.GetComponent<CursorHandler>().timeActivated = waveManager.cooldown;
    }
	
	// Update is called once per frame
	void Update () {

        //si on sait quel gamepad est qui, on y va
        if(player1Mapping != "" && player2Mapping != "")
        {

           // Debug.Log("player 22" + Input.GetAxis("Vertical" + player2Mapping));

            /*if(Input.GetAxis("FuryJP1") > 0)
            {
               
            }
            else if (Input.GetAxis("FuryJP2") > 0)
            {
               

            }*/

            if(Input.GetKeyDown((KeyCode)player1Pal + 4))//LB
            {
                if (GameManager.furyPlayer1 >= 1 && gameManager.stateWave == 1)
                {
                    GameManager.addFury(-GameManager.furyPlayer1, 0);
                    gameManager.ResetBigWave();
                    StartCoroutine(WaitEndAnim(timeAnimationL, false));
                }

                if (!player2IsUlti)
                {
                    player1IsUlti = true;

                    //valide son ulti
                    if (!player1IsUlti)
                    {
                        
                    }


                    //laneSelectedP1 = laneSelectedP1 % playerUltiPoints.Count;

                    Debug.Log(laneSelectedP1);
                }
            }

            
            
            if (Input.GetKeyDown((KeyCode)player2Pal + 4))
            {
                if (GameManager.furyPlayer2 == 1 && gameManager.stateWave == 2)
                {
                    {
                        GameManager.addFury(-GameManager.furyPlayer2, 1);
                    }
                }

                if (!player1IsUlti)
                {
                    player2IsUlti = true;
                }
            }

                if (Input.GetKeyDown((KeyCode)player1Pal))
            {
                waveManager.SendUnit(laneSelectedP1, true);
                player1Cursor.GetComponent<CursorHandler>().Activated();
            }
            else if (Input.GetKeyDown((KeyCode)player2Pal))
            {
                waveManager.SendUnit(laneSelectedP2, false);
                player2Cursor.GetComponent<CursorHandler>().Activated();
            }
            else if (Input.GetAxis("Vertical" + player1Mapping) > 0 && !down1)
            {
                laneSelectedP1 = (laneSelectedP1 + 1) % 3;
                Vector3 pos = player1Cursor.transform.position;
                pos.x = player1Lanes[laneSelectedP1].transform.position.x;

                player1Cursor.GetComponent<CursorHandler>().Move();

                player1Cursor.transform.position = pos;
                down1 = true;
            }
            else if (Input.GetAxis("Vertical" + player1Mapping) < 0 && !down1)
            {
                laneSelectedP1 = (laneSelectedP1 - 1);
               

                if (laneSelectedP1 < 0)
                    laneSelectedP1 = 2;

                Vector3 pos = player1Cursor.transform.position;
                pos.x = player1Lanes[laneSelectedP1].transform.position.x;

                player1Cursor.GetComponent<CursorHandler>().Move();

                player1Cursor.transform.position = pos;

                down1 = true;
            }
            else if (Input.GetAxis("Vertical" + player1Mapping) == 0)
            {
                down1 = false;
            }
             if (Input.GetAxis("Vertical" + player2Mapping) > 0 && !down2)
            {

                laneSelectedP2 = (laneSelectedP2 + 1) % 3;
                Vector3 pos = player2Cursor.transform.position;
                pos.x = player2Lanes[laneSelectedP2].transform.position.x;

                player2Cursor.GetComponent<CursorHandler>().Move();

                player2Cursor.transform.position = pos;

                down2 = true;
            }
            else if (Input.GetAxis("Vertical" + player2Mapping) < 0 && !down2)
            {
                laneSelectedP2 = (laneSelectedP2 - 1);

                if (laneSelectedP2 < 0)
                    laneSelectedP2 = 2;

                Vector3 pos = player2Cursor.transform.position;
                pos.x = player2Lanes[laneSelectedP2].transform.position.x;

                player2Cursor.GetComponent<CursorHandler>().Move();

                player2Cursor.transform.position = pos;

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

    IEnumerator WaitEndAnim(float duration, bool leftSide)
    {
        if (leftSide)
        {
            taLeftUltraWage.PlayAnimation();
        }
        else
        {
            taRightUltraWage.PlayAnimation();
        }
        yield return new WaitForSeconds(duration);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("player1");
        GameObject[] objs2 = GameObject.FindGameObjectsWithTag("player2");

        for (int i = 0; i < objs.Length; i++)
        {
            if (leftSide)
            {
                if (objs[i].transform.position.z < 0)
                {
                    Destroy(objs[i]);
                }
            }
            else
            {
                if (objs[i].transform.position.z > 0)
                {
                    Destroy(objs[i]);
                }
            }
        }
        for (int i = 0; i < objs2.Length; i++)
        {
            if (leftSide)
            {
                if (objs2[i].transform.position.z < 0)
                {
                    Destroy(objs2[i]);
                }
            }
            else
            {
                if (objs2[i].transform.position.z > 0)
                {
                    Destroy(objs2[i]);
                }
            }
        }
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

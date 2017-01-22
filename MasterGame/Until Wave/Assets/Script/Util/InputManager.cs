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

    public int laneSelectedP1 = 1;
    public int laneSelectedP2 = 1;

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

    private int player1Pal = 350;
    private int player2Pal = 370;

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
    [SerializeField]
    AudioSource ultraWaveAudioL;
    [SerializeField]
    AudioSource ultraWaveAudioR;

    [SerializeField]
    GameObject block1;
    [SerializeField]
    GameObject block2;

    public AudioSource fixAudio;


    public int wallPlayer1 = 0;
    public int wallPlayer2 = 0;
    public int wallPrice = 100;
    public GameObject[] wallsPlayer1;
    public GameObject[] wallsPlayer2;


    public int fixPrice;
    public int fixAmount;

    public Castle castleP1;
    public Castle castleP2;

    // Use this for initialization
    void Start () {
        InputManager.moneyPlayer1 = moneyTrue;
        InputManager.moneyPlayer2 = moneyTrue;
        InputManager.staticMoneyForSeaShell = moneyForSeaShell;

        player1Cursor.GetComponent<CursorHandler>().timeActivated = waveManager.cooldown;
        player2Cursor.GetComponent<CursorHandler>().timeActivated = waveManager.cooldown;

        Vector3 pos = player1Cursor.transform.position;
        pos.x = player1Lanes[laneSelectedP1].transform.position.x;
        player1Cursor.transform.position = pos;

         pos = player2Cursor.transform.position;
        pos.x = player2Lanes[laneSelectedP2].transform.position.x;
        player2Cursor.transform.position = pos;
    }
	
	// Update is called once per frame
	void Update () {

       
       
       
           // Debug.Log("player 22" + Input.GetAxis("Vertical" + player2Mapping));

        //repair
        if(Input.GetKeyDown((KeyCode) player1Pal + 4))//LB
        {
            if (moneyPlayer1 >= fixPrice)
            {
                moneyPlayer1 -= fixPrice;

                castleP1.ReceiveFix(fixAmount);

                fixAudio.Play();
            }
        }
        else if(Input.GetKeyDown((KeyCode)player2Pal + 4))
        {
            if (moneyPlayer2 >= fixPrice)
            {
                moneyPlayer2 -= fixPrice;

                castleP2.ReceiveFix(fixAmount);

                fixAudio.Play();
            }
        }

        if (Input.GetKeyDown((KeyCode)player1Pal + 5))//RB
        {
            if (InputManager.moneyPlayer1 - wallPrice >= 0)
            {
                InputManager.addMoney(-wallPrice, 0);
                AddWall(true, 1);
            }
        }
        else if (Input.GetKeyDown((KeyCode)player2Pal + 5))
        {
            if (InputManager.moneyPlayer2 - wallPrice >= 0)
            {
                InputManager.addMoney(-wallPrice, 1);
                AddWall(false, 1);
            }
        }

        if (Input.GetAxis("RightDoorJP1") > 0)//RT
        {

            if (GameManager.furyPlayer1 >= 1 && gameManager.stateWave == 1)
            {
                GameManager.addFury(-GameManager.furyPlayer1, 0);
                gameManager.ResetBigWave();
                StartCoroutine(WaitEndAnim(timeAnimationR, false));
            }
        }
        else if (Input.GetAxis("RightDoorJP2") > 0)
        {
            if (GameManager.furyPlayer2 >= 1 && gameManager.stateWave == 1)
            {
                GameManager.addFury(-GameManager.furyPlayer2, 1);
                gameManager.ResetBigWave();
                StartCoroutine(WaitEndAnim(timeAnimationR, false));
            }
        }

        if (Input.GetAxis("LeftDoorJP1") > 0)//LT
        {

            if (GameManager.furyPlayer1 >= 1 && gameManager.stateWave == 1)
            {
                GameManager.addFury(-GameManager.furyPlayer1, 0);
                gameManager.ResetBigWave();
                StartCoroutine(WaitEndAnim(timeAnimationL, true));
            }
        }
        else if (Input.GetAxis("LeftDoorJP2") > 0)
        {
            if (GameManager.furyPlayer2 >= 1 && gameManager.stateWave == 1)
            {
                GameManager.addFury(-GameManager.furyPlayer2, 1);
                gameManager.ResetBigWave();
                StartCoroutine(WaitEndAnim(timeAnimationL, true));
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
        else if (Input.GetAxis("VerticalJP1") > 0.2f && !down1)
        {
            laneSelectedP1 = (laneSelectedP1 + 1) % 3;
            Vector3 pos = player1Cursor.transform.position;
            pos.x = player1Lanes[laneSelectedP1].transform.position.x;

            player1Cursor.GetComponent<CursorHandler>().Move();

            player1Cursor.transform.position = pos;
            down1 = true;
        }
        else if (Input.GetAxis("VerticalJP1") < -0.2f && !down1)
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
        else if (Input.GetAxis("VerticalJP1") > -0.2f && Input.GetAxis("VerticalJP1") < 0.2f)
        {
            down1 = false;
        }
            if (Input.GetAxis("VerticalJP2") > 0.2f && !down2)
        {

            laneSelectedP2 = (laneSelectedP2 + 1) % 3;
            Vector3 pos = player2Cursor.transform.position;
            pos.x = player2Lanes[laneSelectedP2].transform.position.x;

            player2Cursor.GetComponent<CursorHandler>().Move();

            player2Cursor.transform.position = pos;

            down2 = true;
        }
        else if (Input.GetAxis("VerticalJP2") < -0.2f && !down2)
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
        else if (Input.GetAxis("VerticalJP2") > -0.2f && Input.GetAxis("VerticalJP2") < 0.2f)
        {
            down2 = false;
        }
        
        

        money1Text.text = InputManager.moneyPlayer1.ToString();
        money2Text.text = InputManager.moneyPlayer2.ToString();

        //Debug.Log("Player 1 :" + player1Mapping);
        //Debug.Log("Player 2 :" + player2Mapping);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddWall(true, 1);
        }
	}

    IEnumerator WaitEndAnim(float duration, bool leftSide)
    {
        if (leftSide)
        {
            block1.SetActive(false);
            taLeftUltraWage.PlayAnimation();
            ultraWaveAudioL.Play();
        }
        else
        {
            block2.SetActive(false);
            taRightUltraWage.PlayAnimation();
            ultraWaveAudioR.Play();
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

    public void AddWall(bool isPlayer1, int _value)
    {
        if (isPlayer1)
        {
            if(wallPlayer1 < wallsPlayer1.Length)
            {
                wallPlayer1 = wallPlayer1 + _value;
            }

            for (int i = 1; i < wallsPlayer1.Length+1; i++)
            {
                if(i<= wallPlayer1)
                {
                    wallsPlayer1[i-1].SetActive(true);
                }
                else
                {
                    wallsPlayer1[i-1].SetActive(false);
                }
            }
        }
        else
        {
            if (wallPlayer2 < wallsPlayer1.Length)
            {
                wallPlayer2 = wallPlayer2 + _value;
            }

            for (int i = 1; i < wallsPlayer2.Length + 1; i++)
            {
                if (i <= wallPlayer2)
                {
                    wallsPlayer2[i - 1].SetActive(true);
                }
                else
                {
                    wallsPlayer2[i - 1].SetActive(false);
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

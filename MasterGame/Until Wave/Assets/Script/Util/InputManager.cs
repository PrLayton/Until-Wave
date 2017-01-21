using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
  static  public int moneyPlayer1;
    static public int moneyPlayer2;
    public int moneyTrue;
    static public int staticMoneyForSeaShell;
    public int moneyForSeaShell;

    public int laneSelected = 2;

    public WaveManager waveManager;


    private bool down = false;
	// Use this for initialization
	void Start () {
        InputManager.moneyPlayer1 = moneyTrue;
        InputManager.moneyPlayer2 = moneyTrue;
        InputManager.staticMoneyForSeaShell = moneyForSeaShell;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            waveManager.SendUnit(laneSelected, true);
        }
        else if(Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            waveManager.SendUnit(laneSelected, false);
        }
        else if (Input.GetAxis("VerticalJP1") > 0 && !down)
        {
            laneSelected = (laneSelected + 1) % 3;
            down = true;
        }
        else if (Input.GetAxis("VerticalJP1") < 0 && !down)
        {
            laneSelected = (laneSelected - 1);

            if (laneSelected < 0)
                laneSelected = 2;

            down = true;
        }
        else if(Input.GetAxis("VerticalJP1") == 0)
        {
            down = false;
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

        Debug.Log(moneyPlayer1);
    }
}

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
  static  public int moneyPlayer1;
    static public int moneyPlayer2;
    public int moneyTrue;
    static public int staticMoneyForSeaShell;
    public int moneyForSeaShell;

    public int laneSelected;

    public WaveManager waveManager;

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
            waveManager.SendUnit(laneSelected);
        }
        else if(Input.GetAxis("y axis") > 0)
        {
            laneSelected = (laneSelected + 1) % 3;
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

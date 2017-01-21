using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {
  static  public int moneyPlayer1;
    static public int moneyPlayer2;
    public int moneyTrue;
    static public int staticMoneyForSeaShell;
    public int moneyForSeaShell;

    public int laneSelected = 2;

    public WaveManager waveManager;

    public Text money1Text;
    public Text money2Text;

    private bool down = false;
	// Use this for initialization
	void Start () {
        InputManager.moneyPlayer1 = moneyTrue;
        InputManager.moneyPlayer2 = moneyTrue;
        InputManager.staticMoneyForSeaShell = moneyForSeaShell;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            waveManager.SendUnit(laneSelected, true);
        }
        if(Input.GetKeyDown(KeyCode.Joystick1Button0))
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

        money1Text.text = InputManager.moneyPlayer1.ToString();
        money2Text.text = InputManager.moneyPlayer2.ToString();
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

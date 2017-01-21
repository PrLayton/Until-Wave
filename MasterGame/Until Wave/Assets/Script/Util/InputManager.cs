using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
  static  public int money;
    public int moneyTrue;

    public int laneSelected = 2;

    public WaveManager waveManager;


    private bool down = false;
	// Use this for initialization
	void Start () {
        InputManager.money = moneyTrue;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            waveManager.SendUnit(laneSelected);
        }
        else if(Input.GetAxis("VerticalJP1") > 0 && !down)
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

    static void addMoney(int _money)
    {
        money += _money;
    }
}

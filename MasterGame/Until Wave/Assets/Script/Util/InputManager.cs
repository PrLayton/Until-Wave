using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
  static  public int money;
    public int moneyTrue;

    public int laneSelected;

    public WaveManager waveManager;

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
        else if(Input.GetAxis("y axis") > 0)
        {
            laneSelected = (laneSelected + 1) % 3;
        }
	}

    static void addMoney(int _money)
    {
        money += _money;
    }
}

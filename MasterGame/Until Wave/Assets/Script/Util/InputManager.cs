using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    public int money;

    public int laneSelected;

    public WaveManager waveManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if(Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            waveManager.SendUnit(laneSelected);
        }
        /*else if(Input.GetAxis())
        {
            laneSelected = (laneSelected + 1) % 3;
        }*/
	}

    public void addMoney(int _money)
    {
        money += _money;
    }
}

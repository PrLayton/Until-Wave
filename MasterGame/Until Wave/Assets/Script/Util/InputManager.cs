using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

   static public int money;

    public short laneSelected;

    public WaveManager waveManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("0"))
        {
            
        }
	}

    static void addMoney(int _money)
    {
        InputManager.money += _money;
    }
}

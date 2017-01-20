using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {

    public GameObject[] pool1;
    public GameObject[] pool2;

    float timer = 0;

	// Use this for initialization
	void Start () {
	
	}

    public void SendUnit(){
        foreach (GameObject item in pool1)
        {
            if (!item.activeSelf)
            {
                item.SetActive(true);
                break;
            }
        }
        foreach (GameObject item in pool2)
        {
            if (!item.activeSelf)
            {
                item.SetActive(true);
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer-= Time.deltaTime;
        if(timer < 0)
        {
            timer = 4;
        }

	}


}

using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

    public GameObject prefabUnit;

    public Transform[] spawnersPlayer1;
    public Transform[] spawnersPlayer2;

    void Start () {
	
	}

    public void SendUnit(int line){
        GameObject tmp =  GameObject.Instantiate(prefabUnit, spawnersPlayer1[line].transform.position, spawnersPlayer1[line].transform.rotation) as GameObject;
        tmp.tag = "player1";
        GameObject tmp2 = GameObject.Instantiate(prefabUnit, spawnersPlayer2[line].transform.position, spawnersPlayer2[line].transform.rotation) as GameObject;
        tmp2.tag = "player2";
    }
	
	void Update () {

	}


}

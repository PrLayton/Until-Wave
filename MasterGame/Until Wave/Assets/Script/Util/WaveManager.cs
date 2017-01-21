﻿using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

    public GameObject prefabUnit;
    public int pricePrefabUnit;

    public Transform[] spawnersPlayer1;
    public Transform[] spawnersPlayer2;

    public float cooldown;
    float currentCooldownPlayer1;
    float currentCooldownPlayer2;

    void Start () {
        currentCooldownPlayer1 = 0;
        currentCooldownPlayer2 = 0;
    }

    public void SendUnit(int line, bool isplayer1){
        if (isplayer1)
        {
            if (currentCooldownPlayer1 <= 0.0f && InputManager.moneyPlayer1 - pricePrefabUnit >= 0)
            {
                InputManager.addMoney(-pricePrefabUnit, 0);
                GameObject tmp = GameObject.Instantiate(prefabUnit, spawnersPlayer1[line].transform.position, spawnersPlayer1[line].transform.rotation) as GameObject;
                tmp.tag = "player1";

                currentCooldownPlayer1 = cooldown;
            }
        }
        else
        {
            if(currentCooldownPlayer2 <= 0.0f && InputManager.moneyPlayer2 - pricePrefabUnit >= 0)
            {
                InputManager.addMoney(-pricePrefabUnit, 0);
                GameObject tmp2 = GameObject.Instantiate(prefabUnit, spawnersPlayer2[line].transform.position, spawnersPlayer2[line].transform.rotation) as GameObject;
                tmp2.tag = "player2";

                currentCooldownPlayer2 = cooldown;
            }
        }
    }
	
	void Update () {
        currentCooldownPlayer1 -= Time.deltaTime;
        currentCooldownPlayer2 -= Time.deltaTime;
    }


}

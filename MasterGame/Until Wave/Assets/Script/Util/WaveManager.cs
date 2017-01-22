using UnityEngine;
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
                tmp.layer = 13;

                tmp.GetComponent<Unit>().meshMP2.SetActive(false);
                tmp.GetComponent<Unit>().meshFP2.SetActive(false);

                if (Random.Range(0.0f,1.0f) >= 0.5)
                {
                    tmp.GetComponent<Unit>().meshMP1.SetActive(false);
                    tmp.GetComponent<Unit>().isAGirl = true;
                }
                else
                {
                    tmp.GetComponent<Unit>().meshFP1.SetActive(false);
                    tmp.GetComponent<Unit>().isAGirl = false;
                }

                currentCooldownPlayer1 = cooldown;
            }
        }
        else
        {
            if(currentCooldownPlayer2 <= 0.0f && InputManager.moneyPlayer2 - pricePrefabUnit >= 0)
            {
                InputManager.addMoney(-pricePrefabUnit, 1);
                GameObject tmp2 = GameObject.Instantiate(prefabUnit, spawnersPlayer2[line].transform.position, spawnersPlayer2[line].transform.rotation) as GameObject;
                tmp2.tag = "player2";
                tmp2.layer = 14;

                if (Random.Range(0.0f, 1.0f) >= 0.5)
                {
                    tmp2.GetComponent<Unit>().meshMP2.SetActive(false);
                    tmp2.GetComponent<Unit>().isAGirl = true;
                }
                else
                {
                    tmp2.GetComponent<Unit>().meshFP2.SetActive(false);
                    tmp2.GetComponent<Unit>().isAGirl = false;
                }

                tmp2.GetComponent<Unit>().InverseSpeed();
                currentCooldownPlayer2 = cooldown;
            }
        }
    }

    public void SendUnit(int line)
    {
        GameObject tmp = GameObject.Instantiate(prefabUnit, spawnersPlayer1[line].transform.position, spawnersPlayer1[line].transform.rotation) as GameObject;
        tmp.tag = "player1";
        tmp.layer = 13;
        GameObject tmp2 = GameObject.Instantiate(prefabUnit, spawnersPlayer2[line].transform.position, spawnersPlayer2[line].transform.rotation) as GameObject;
        tmp2.tag = "player2";
        tmp2.layer = 14;
        tmp2.GetComponent<Unit>().InverseSpeed();

        if (Random.Range(0.0f, 1.0f) >= 0.5)
        {
           // tmp.GetComponent<Unit>().meshM.SetActive(false);
            tmp.GetComponent<Unit>().isAGirl = true;
        }
        else
        {
           // tmp.GetComponent<Unit>().meshF.SetActive(false);
            tmp.GetComponent<Unit>().isAGirl = false;
        }

        if (Random.Range(0.0f, 1.0f) >= 0.5)
        {
           // tmp2.GetComponent<Unit>().meshM.SetActive(false);
            tmp2.GetComponent<Unit>().isAGirl = true;
        }
        else
        {
           // tmp2.GetComponent<Unit>().meshF.SetActive(false);
            tmp2.GetComponent<Unit>().isAGirl = false;
        }
    }
    
    void Update () {
        currentCooldownPlayer1 -= Time.deltaTime;
        currentCooldownPlayer2 -= Time.deltaTime;
    }


}

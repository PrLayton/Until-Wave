using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tuto : MonoBehaviour {

    public GameObject[] ecrans_tuto;
    private int indice = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (indice < ecrans_tuto.Length - 1)
            {
                ecrans_tuto[indice].SetActive(false);
                indice++;
                ecrans_tuto[indice].SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("Testscene");
            }
        }
        
    }
}

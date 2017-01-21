using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public GameObject selector;
    public GameObject startText;
    public GameObject quitText;
    public AudioClip tchick;
    public int optionSelected = 1;

    private string player1Mapping = "";
    private int player1Pal = -1;
    private bool down = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // si le joueur 1 est mappé on y va
        if (player1Mapping != "")
        {

            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                AudioSource.PlayClipAtPoint(tchick, selector.transform.position);

                if (optionSelected == 1)
                {
                    SceneManager.LoadScene("TestScene");
                }
                else
                {
                    Application.Quit();
                }
            }
            else if ((Input.GetAxis("Vertical" + player1Mapping) > 0 || Input.GetAxis("Vertical" + player1Mapping) < 0) && !down)
            {
                AudioSource.PlayClipAtPoint(tchick, selector.transform.position);
                if (optionSelected == 1)
                {
                    optionSelected = 2;
                    selector.transform.position = quitText.transform.position - new Vector3(310, 0, 0);
                }
                else
                {
                    optionSelected = 1;
                    selector.transform.position = startText.transform.position - new Vector3(210, 0, 0);
                }
                down = true;
            }
            else if (Input.GetAxis("Vertical" + player1Mapping) == 0)
                down = false;

        }
        else // on map le joueur 1
        {
            for (int i = 350; i <= 490; i += 20)
            {
                if (Input.GetKeyDown((KeyCode)i))
                {
                    if (player1Mapping == "")
                    {
                        player1Mapping = "JP" + (((i - 350) / 20) + 1);
                        player1Pal = i;
                    }
                }
            }
        }
    }
}

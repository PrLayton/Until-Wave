using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public GameObject selector;
    public GameObject startText;
    public GameObject quitText;
    public AudioClip tchick;
    public AudioClip intro;
    public AudioClip ambianceSea;
    public GameObject blackscreen;
    public int optionSelected = 1;

    private string player1Mapping = "";
    private int player1Pal = -1;

    private float timeElapsed = 0;

    private bool down = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        timeElapsed += Time.deltaTime;

        if (timeElapsed > intro.length - 0.01 && timeElapsed < intro.length + 0.01)
            AudioSource.PlayClipAtPoint(intro, new Vector3(0, 0, 1));

        if (timeElapsed > intro.length * 2)
        {
            if (blackscreen.active)
            {
                blackscreen.SetActive(false);
                AudioSource.PlayClipAtPoint(ambianceSea, new Vector3(0, 0, 0));
            }     
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            AudioSource.PlayClipAtPoint(tchick, new Vector3(0, 0, 1));

            if (optionSelected == 1)
            {
                SceneManager.LoadScene("TestScene");
            }
            else
            {
                Application.Quit();
            }
        }
        else if (Input.GetAxis("VerticalJP1") > 0.5f)
        {
            if(!down)
            {
                AudioSource.PlayClipAtPoint(tchick, new Vector3(0, 0, 1));
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
            }
            
        }
        else if ((Input.GetAxis("VerticalJP1") < -0.5f))
        {
            if (!down)
            {
                AudioSource.PlayClipAtPoint(tchick, new Vector3(0, 0, 1));
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
        }
        else if(Input.GetAxis("VerticalJP1") < 0.2f || Input.GetAxis("VerticalJP1") > -0.2f)
        {
            down = false;
        }

    }
}

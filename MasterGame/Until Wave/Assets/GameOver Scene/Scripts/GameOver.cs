using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {

    int victory;
    public GameObject max_win;
    public GameObject sam_win;
    public GameObject times_up;

    public AudioClip victory_jingle;
    public AudioClip[] sam_victory_sound;
    public AudioClip[] max_victory_sound;
    public AudioClip[] sam_defeat_sound;
    public AudioClip[] max_defeat_sound;

    private float random2;
    private float random3;

    private float timeElapsed = 0;


    // Use this for initialization
    void Start () {

        AudioSource.PlayClipAtPoint(victory_jingle, new Vector3(0, 0, 1));

        random2 = Mathf.Floor(Random.Range(0, 2));
        random3 = Mathf.Floor(Random.Range(0, 3));
        victory = PlayerPrefs.GetInt("Winning");
        print("victory : " + victory);
        if ((EWin)victory == EWin.WinTimePlayer1 || (EWin)victory == EWin.WinTimePlayer2)
            times_up.SetActive(true);
    }

    void Update ()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > victory_jingle.length - 2.5 && timeElapsed < victory_jingle.length - 2.48)
        {
            if ((EWin)victory == EWin.WinCastlePlayer1 || (EWin)victory == EWin.WinTimePlayer1)
            {
                maxWin();
            }
            else if ((EWin)victory == EWin.WinCastlePlayer2 || (EWin)victory == EWin.WinTimePlayer2)
            {
                samWin();
            }
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    void maxWin()
    {
        max_win.SetActive(true);
        AudioSource.PlayClipAtPoint(max_victory_sound[(int)random2], new Vector3(0, 0, 1));
        AudioSource.PlayClipAtPoint(sam_defeat_sound[(int)random2], new Vector3(0, 0, 1));
    }

    void samWin()
    {
        sam_win.SetActive(true);
        AudioSource.PlayClipAtPoint(sam_victory_sound[(int)random3], new Vector3(0, 0, 1));
        AudioSource.PlayClipAtPoint(max_defeat_sound[(int)random2], new Vector3(0, 0, 1));

    }
}

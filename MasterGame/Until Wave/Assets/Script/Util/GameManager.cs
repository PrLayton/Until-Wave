using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float timerWave;
    float currentTimerWave;
    public float timeAnim;
    public float timerandomVariation;
    public float probSpawnShell;
    public int moneyPerShell;

    public GameObject prefabShell;

    public Transform[] sellPositions;

    public float generalTimer;
    float currentGeneralTimer;
    public Text generalTimerText;

    public int xShellPerSecond;
    public int yShellPerSecond;
    public int zShellPerSecond;

    float timerShell1Second = 1f;
    float timerShell30Second = 30f;

    [SerializeField]
    Image furyContent1;
    [SerializeField]
    Image furyContent2;
    public float furyFillAmount1;
    public float furyFillAmount2;

    //public static List<AudioSource> staticFeedbacksSoundAttack;
    //public List<AudioSource> feedbacksSoundAttack;
    //public GameObject camera;

    // Use this for initialization
    void Start () {

        currentTimerWave = timerWave;
        currentGeneralTimer = generalTimer;
        //AudioSource[] aSources = camera.GetComponents<AudioSource>();
        //staticFeedbacksSoundAttack = feedbacksSoundAttack;
        //staticFeedbacksSoundAttack.Add(aSources[0]);
    }
	
	// Update is called once per frame
	void Update () {
        currentTimerWave -= Time.deltaTime;
        if (currentTimerWave + Random.Range(-timerandomVariation, timerandomVariation) <= 0)
        {
            StartCoroutine(SendShells());
            currentTimerWave = timerWave;
        }
        currentGeneralTimer -= Time.deltaTime;

        generalTimerText.text = currentGeneralTimer.ToString();

        timerShell1Second-= Time.deltaTime;
        if (timerShell1Second <= 0)
        {
            InputManager.addMoney(xShellPerSecond + yShellPerSecond * zShellPerSecond, 0);
            InputManager.addMoney(xShellPerSecond + yShellPerSecond * zShellPerSecond, 1);
            timerShell1Second = 1.0f;
        }
        timerShell30Second -= Time.deltaTime;
        if (timerShell30Second <= 0)
        {
            zShellPerSecond++;
            timerShell30Second = 30.0f;
        }

        furyContent1.fillAmount = furyFillAmount1;
        furyContent2.fillAmount = furyFillAmount2;

        generalTimerText.text = string.Format("{0}:{1}", ((int)currentGeneralTimer) / 60,  ((int)currentGeneralTimer) % 60);

    }

    IEnumerator SendShells()
    {
        yield return new WaitForSeconds(timeAnim);
        foreach (var item in sellPositions)
        {
            if(Random.Range(0.0f, 1f) <= probSpawnShell)
            {
                GameObject.Instantiate(prefabShell, item.transform.position, Quaternion.identity);
            }
        }
    }

   public static void EndGame(bool isCastleDead, bool isPlayer1Winner)
    {
        SceneManager.LoadSceneAsync(isCastleDead ? 0 : 1);

        PlayerPrefs.SetInt("Winning", isPlayer1Winner ? 0 : 1);
    }
}

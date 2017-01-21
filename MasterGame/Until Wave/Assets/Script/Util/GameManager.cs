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
    public float furyFillAmount1 = 0;
    public float furyFillAmount2 = 0;

    //public static List<AudioSource> staticFeedbacksSoundAttack;
    //public List<AudioSource> feedbacksSoundAttack;
    //public GameObject camera;

    public float timeAnimBigWave;
    public float bigWaveTimer = 60;
    public Castle castle1;
    public Castle castle2;

    static public float furyPlayer1;
    static public float furyPlayer2;

    public int stateWave = -1;
    public SpriteRenderer sprtBigWaveRenderer;
    public Sprite[] sprtBigWave;

    // Use this for initialization
    void Start () {

        currentTimerWave = timerWave;
        currentGeneralTimer = generalTimer;
        //AudioSource[] aSources = camera.GetComponents<AudioSource>();
        //staticFeedbacksSoundAttack = feedbacksSoundAttack;
        //staticFeedbacksSoundAttack.Add(aSources[0]);
        furyPlayer1 = 0;
        furyPlayer2 = 0;
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

        furyContent1.fillAmount = furyPlayer1;
        furyContent2.fillAmount = furyPlayer2;

        generalTimerText.text = string.Format("{0}:{1}", ((int)currentGeneralTimer) / 60,  ((int)currentGeneralTimer) % 60);

        bigWaveTimer -= Time.deltaTime;
        if (bigWaveTimer <= 0)
        {
            StartCoroutine(WaitAnimBigWave());
            if(stateWave < sprtBigWave.Length-1)
            {
                stateWave++;
                sprtBigWaveRenderer.sprite = sprtBigWave[stateWave];
            }
            bigWaveTimer = 60.0f;
        }
    }

    IEnumerator WaitAnimBigWave()
    {
        yield return new WaitForSeconds(timeAnimBigWave);
        //castle1.ReceiveDamage(5);
        //castle2.ReceiveDamage(5);
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

    static public void addFury(float _value, int _player)
    {
        if (_player == 0)
        {
            furyPlayer1 += _value;
        }
        else
        {
            furyPlayer2 += _value;
        }
    }
}

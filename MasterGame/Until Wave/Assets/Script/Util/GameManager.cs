using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum EWin
{
    WinCastlePlayer1 = 0,
    WinCastlePlayer2,
    WinTimePlayer1,
    WinTimePlayer2
}
public class GameManager : MonoBehaviour {

    public float timerLittleWave;
    float currentTimerWave;
    public float timeAnimLittleWave;
    public float timerandomVariation;
    bool littleWaveLaunched;
    bool shellLaunched;

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
    public int bigWaveDammage = 20;

    static public float furyPlayer1;
    static public float furyPlayer2;

    public int stateWave = -1;
    public SpriteRenderer sprtBigWaveRenderer;
    //public Sprite[] sprtBigWave;
    public GameObject empyWave;
    public Transform[] empyWavePositions;
    public Transform empyWaveStartPosition;

    public ToolAnimator animatorForLittleWave;
    public ToolAnimator animatorForBigWave;

    public AudioSource littleWaveAudio;
    public AudioSource bigWaveAudio;
    public AudioSource addWaveStateAudio;

    [SerializeField]
    GameObject block1;
    [SerializeField]
    GameObject block2;

    [SerializeField]
    InputManager inputManager;

    // Use this for initialization
    void Start () {
        currentTimerWave = 0;
        currentGeneralTimer = generalTimer;
        //AudioSource[] aSources = camera.GetComponents<AudioSource>();
        //staticFeedbacksSoundAttack = feedbacksSoundAttack;
        //staticFeedbacksSoundAttack.Add(aSources[0]);
        furyPlayer1 = 0;
        furyPlayer2 = 0;
        littleWaveLaunched = false;
        shellLaunched = true;
    }
	
	// Update is called once per frame
	void Update () {
        currentTimerWave -= Time.deltaTime;

        if (!littleWaveLaunched)
        {
            if (currentTimerWave - timeAnimLittleWave <= 0)
            {
                StartCoroutine(SendShells());
                //animatorForLittleWave.PlayAnimation();
                littleWaveLaunched = true;
                // shellLaunched = false;
            }
        }


        if (currentTimerWave <= 0)
        {
            currentTimerWave = timerLittleWave;//+ Random.Range(-timerandomVariation, timerandomVariation);
            littleWaveLaunched = false;
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
            bigWaveTimer = 60.0f;
        }

        //empyWave.transform.position = Vector3.Lerp(empyWavePosition1.position, empyWavePosition2.position, Time.deltaTime * 10f);
        
        if (currentGeneralTimer <= 0)
            EndGame(false, castle1.health >= castle2.health ? true : false);
    }

    IEnumerator WaitAnimBigWave()
    {
        block1.SetActive(true);
        block2.SetActive(true);
        animatorForBigWave.PlayAnimation();
        bigWaveAudio.Play();
        yield return new WaitForSeconds(timeAnimBigWave);
        if (stateWave < empyWavePositions.Length - 1)
        {
            stateWave++;
            empyWave.transform.position = empyWavePositions[stateWave].position;
            //sprtBigWaveRenderer.sprite = sprtBigWave[stateWave];
            addWaveStateAudio.Play();
        }
        castle1.ReceiveDamage(bigWaveDammage/(inputManager.wallPlayer1+1));
        castle2.ReceiveDamage(bigWaveDammage/(inputManager.wallPlayer1+1));
    }

    public void ResetBigWave()
    {
        stateWave = - 1;
        empyWave.transform.position = empyWaveStartPosition.position;
        //sprtBigWaveRenderer.sprite = null;
    }

    IEnumerator SendShells()
    {
        animatorForLittleWave.PlayAnimation();
        littleWaveAudio.Play();
        yield return new WaitForSeconds(timeAnimLittleWave- timeAnimLittleWave/1.8f);
        foreach (var item in sellPositions)
        {
            if (Random.Range(0.0f, 1f) <= probSpawnShell)
            {
                GameObject.Instantiate(prefabShell, item.transform.position, Quaternion.identity);
            }
        }
    }

   public static void EndGame(bool isCastleDead, bool isPlayer1Winner)
    {

        SceneManager.LoadSceneAsync(2);

        if(isCastleDead)
        {
            PlayerPrefs.SetInt("Winning", isPlayer1Winner ? (int)EWin.WinCastlePlayer1 : (int)EWin.WinCastlePlayer2);

        }
        else//timer
        {          
           PlayerPrefs.SetInt("Winning", isPlayer1Winner ? (int)EWin.WinTimePlayer1 : (int)EWin.WinTimePlayer2);
        }

        PlayerPrefs.Save();
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
        if (furyPlayer1 > 1)
        {
            furyPlayer1 = 1;
        }
        if (furyPlayer2 > 1)
        {
            furyPlayer2 = 1;
        }
    }
}

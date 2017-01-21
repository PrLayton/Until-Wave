using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float timerWave;
    float currentTimerWave;
    public float timeAnim;
    public float timerandomVariation;
    public float probSpawnShell;

    public GameObject prefabShell;

    public Transform[] sellPositions;

    public float generalTimer;
    float currentGeneralTimer;
    public Text generalTimerText;

    // Use this for initialization
    void Start () {

        currentTimerWave = timerWave;
        currentGeneralTimer = generalTimer;
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

   public static void EndGame(bool isCastleDead)
    {
        if(isCastleDead)
        {
            SceneManager.LoadSceneAsync(0);
        }
        else
        {

        }
    }
}

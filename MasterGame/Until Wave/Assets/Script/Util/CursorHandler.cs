using UnityEngine;
using System.Collections;

public class CursorHandler : MonoBehaviour {

    public Sprite cursorNormal;
    public Sprite cursorActivated;

    public AudioClip cursorSound;

    public WaveManager waveManager;

    public float timeActivated;

    private bool activated;

    private float timeEnd;

    // Use this for initialization
    private void Start()
    {
        activated = false;
        GetComponent<SpriteRenderer>().sprite = cursorNormal;

        timeActivated = waveManager.cooldown;
    }
    // Update is called once per frame
    void Update () {
        if (timeEnd < Time.time)
        {
            activated = false;
            GetComponent<SpriteRenderer>().sprite = cursorNormal;
        }
	}

    public void Activated()
    {

        timeEnd = Time.time + timeActivated;

        GetComponent<SpriteRenderer>().sprite = cursorActivated;
    }

    public void Move()
    {
        AudioSource.PlayClipAtPoint(cursorSound, gameObject.transform.position);
    }
}

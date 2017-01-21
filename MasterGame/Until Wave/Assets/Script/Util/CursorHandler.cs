using UnityEngine;
using System.Collections;

public class CursorHandler : MonoBehaviour {

    public Sprite cursorNormal;
    public Sprite cursorActivated;

    public AudioClip cursorSound;

    public float timeActivated;

    private bool activated;

    private float timeEnd;

    // Use this for initialization
    private void Start()
    {
        

        activated = false;
        GetComponent<SpriteRenderer>().sprite = cursorNormal;
    }
    // Update is called once per frame
    void Update () {

	    if(activated)
        {
            if (timeEnd < Time.time)
            {
                activated = false;
                GetComponent<SpriteRenderer>().sprite = cursorNormal;
            }
        }
	}

    public void Activated()
    {
        activated = true;

        timeEnd = Time.time + timeActivated;

        GetComponent<SpriteRenderer>().sprite = cursorActivated;
    }

    public void Move()
    {
        AudioSource.PlayClipAtPoint(cursorSound, gameObject.transform.position);
    }
}

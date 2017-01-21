using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolAnimator : MonoBehaviour {

    [SerializeField]
    SpriteRenderer sprR;

    public Sprite[] sprites;
    float framesPorSegundo = 2.0f;
    private int index;

    bool alreadyLaunched = true;
    float savedTime;

    [SerializeField]
    bool loop = true;

    bool alreadySaveTime = false;

    // Use this for initialization
    void Start () {
        //alreadyLaunched = false;
    }

    public void PlayAnimation()
    {
        alreadyLaunched = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!alreadyLaunched)
        {
            if (!alreadySaveTime)
            {
                savedTime = Time.time;
                alreadySaveTime = true;
            }
            if (index < sprites.Length - 1)
            {
                index = (int)((Time.time - savedTime) * framesPorSegundo);
                index = index % sprites.Length;
                sprR.sprite = sprites[index];
            }
            else
            {
                if (!loop)
                {
                    alreadyLaunched = true;
                }
                index = 0;
            }
        }

    }
}

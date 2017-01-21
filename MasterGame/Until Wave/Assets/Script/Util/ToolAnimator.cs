using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolAnimator : MonoBehaviour {

    [SerializeField]
    RawImage img;
    [SerializeField]
    SpriteRenderer sprR;
    [SerializeField]
    bool useSprtite = false;

    public Texture2D[] frames;
    public Sprite[] sprites;
    float framesPorSegundo = 20.0f;
    private int index;

    bool alreadyLaunched = true;
    float savedTime;

    [SerializeField]
    bool loop;

    bool alreadySaveTime = false;

    public void PlayAnim()
    {
        //savedTime = Time.time;
        alreadyLaunched = false;
    }

    // Use this for initialization
    void Start () {
        //alreadyLaunched = false;
    }

    public void PlayAndLoopAnimation()
    {
        loop = true;
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
            if (useSprtite)
            {
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
                }
            }
            else
            {
                if (index < frames.Length - 1)
                {
                    index = (int)((Time.time - savedTime) * framesPorSegundo);
                    index = index % frames.Length;
                    img.texture = frames[index];
                }
                else
                {
                    if (!loop)
                    {
                        alreadyLaunched = true;
                    }
                }
            }
        }

    }
}

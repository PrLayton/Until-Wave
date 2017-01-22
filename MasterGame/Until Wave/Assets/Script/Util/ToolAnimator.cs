using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolAnimator : MonoBehaviour {

    [SerializeField]
    SpriteRenderer sprR;

    public Sprite[] sprites;
    float framesPorSegundo = 2.0f;
    private int index;

    //combien de temps dure l'anim
    public float secAnim;
    private float cycleAnim;

    //indique toutes les combiens de secondes on a l'animation
    public float timeCycleAnim;

    bool alreadyLaunched = true;
    float savedTime;

    [SerializeField]
    bool loop = false;

    bool alreadySaveTime = false;

    private float timeImage = 0.0f;
    private float timeAnim = 0.0f;
    private int indexImage;

    // Use this for initialization
    void Start () {
        //alreadyLaunched = false;
    }

    public void PlayAnimation()
    {
        alreadyLaunched = false;
        alreadySaveTime = false;
        index = 0;

        cycleAnim = secAnim / sprites.Length;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        
       /* timeAnim += Time.deltaTime;

        //need to anim
        if(timeAnim >= timeCycleAnim)
        {
            timeImage += Time.deltaTime;

            if (timeImage >= cycleAnim)
            {
                indexImage = (indexImage + 1) % sprites.Length;

                sprR.sprite = sprites[index];

                timeImage = 0.0f;
            }

            timeAnim = 0.0f;
        }*/
        

        if (!alreadyLaunched)
        {
            if (!alreadySaveTime)
            {
                savedTime = Time.time;
                alreadySaveTime = true;
            }
            if (index < sprites.Length-1)
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
                sprR.sprite = null;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    [SerializeField]
    [Tooltip("Vitesse de l'unité")]
    int speed;
    [SerializeField]
    int life;
    [SerializeField]
    int attack;

    [SerializeField]
    [Tooltip("Vitesse d'attaque de l'unité")]
    float timerAttack = 3;

    float currentTimerAttack = 0;

    public AudioSource[] feedbacksSoundAttack;
    public AudioSource[] feedbacksSoundWalk;
    public AudioSource feedbacksSounShell;

    float timerWalkFeedbackSound = 1;

    enum State
    {
        walk,
        fight
    }

    State unitState;

    Unit currentEnemy;

    // Use this for initialization
    void Start () {
        unitState = State.walk;
        currentTimerAttack = 0;
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (unitState == State.fight)
        {
            if (currentEnemy == null)
            {
                unitState = State.walk;
            }

            currentTimerAttack -= Time.deltaTime;
            if (currentTimerAttack <= 0)
            {
                Attack();
                currentTimerAttack = timerAttack;
            }
        }
        else
        {
            timerWalkFeedbackSound -= Time.deltaTime;

            if (timerWalkFeedbackSound<= 0.0f)
            {
                feedbacksSoundWalk[Random.Range(0, feedbacksSoundWalk.Length)].Play();
                timerWalkFeedbackSound = 1f;
            }
        }
   
    }

    public Vector3 teleportPoint;
    public Rigidbody rb;
 
    void FixedUpdate()
    {
        //rb.MovePosition(transform.position + transform.forward * Time.deltaTime);
        //print(rb.velocity);
       /* if(rb.velocity.y < -1)
        {
            rb.velocity.Set(0, 0, 0);
        }*/
        if (unitState == State.walk)
        {
            //this.GetComponent<Rigidbody>().AddForce(this.transform.forward, ForceMode.Force);
            this.transform.Translate(this.transform.forward * 0.01f * speed);
        }
        //print(Mathf.Round(transform.eulerAngles.x));
        /*if (transform.eulerAngles.x < -100f) {
            transform.Rotate(new Vector3(-5, 0, 0));
        }*/
        //if (transform.eulerAngles.x > 100f) { transform.Rotate(new Vector3(5, 0, 0)); }
        //this.transform.Rotate(this.transform.right, -transform.localRotation.eulerAngles.x*Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject, 0.3f);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if ((this.gameObject.tag == "player1" && collision.gameObject.tag == "player2") || this.gameObject.tag == "player2" && collision.gameObject.tag == "player1")
            if (unitState == State.walk)
            {
                currentEnemy = collision.gameObject.GetComponent<Unit>();
                unitState = State.fight;
            }
        if(collision.gameObject.tag == "seaShell")
        {
            if(this.gameObject.tag == "player1")
            {
                InputManager.addMoney(InputManager.staticMoneyForSeaShell, 0);
            }
            else
            {
                InputManager.addMoney(InputManager.staticMoneyForSeaShell, 1);
            }
            Destroy(collision.gameObject);
            feedbacksSounShell.Play();
        }
    }

    void Attack()
    {
        feedbacksSoundAttack[Random.Range(0, feedbacksSoundAttack.Length)].Play();
        currentEnemy.LoseLife(attack);
    }

    public void LoseLife(int _attack)
    {
        life -= _attack;
    }

    public void InverseSpeed()
    {
        speed = -speed;
    }
}

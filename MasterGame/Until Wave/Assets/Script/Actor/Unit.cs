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
    public AudioSource feedbacksSoundAttackCastle;

    public AudioSource[] feedbacksSoundDeadM;
    public AudioSource[] feedbacksSoundDeadF;

    [Tooltip("Spawné quand shell picked-up")]
    public ParticleSystem particleShell;

    float timerWalkFeedbackSound = 1;

    enum State
    {
        walk = 0,
        fight,
        idle,
        dead
    }

    State unitState;

    Unit currentEnemy;

    public bool isAGirl;

    public GameObject meshM;
    public GameObject meshF;

    private Castle enemyCastle;
    // Use this for initialization
    void Start () {
        unitState = State.walk;
        currentTimerAttack = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (unitState == State.fight)
        {
            if (currentEnemy == null && enemyCastle == null)
            {
                unitState = State.walk;
            }

            currentTimerAttack -= Time.deltaTime;
            if (currentTimerAttack <= 0)
            {
                if (currentEnemy != null)
                    AttackUnit();
                else if (enemyCastle != null)
                    AttackCastle();

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
 
    void FixedUpdate()
    {
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
        if (life <= 0 && unitState != State.dead)
        {
            if(this.gameObject.tag == "player1")
            {
                GameManager.addFury(0.05f, 1);
            } else
            {
                GameManager.addFury(0.05f, 0);
            }

            if (this.gameObject.tag == "player1")
            {
                feedbacksSoundDeadM[Random.Range(0, feedbacksSoundDeadM.Length)].Play();
            }
            else
            {
                feedbacksSoundDeadF[Random.Range(0, feedbacksSoundDeadF.Length)].Play();
            }

            Destroy(this.gameObject, 0.5f);
            unitState = State.dead;
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

         if (collision.gameObject.tag == "castle")
        {
            enemyCastle = collision.gameObject.GetComponent<Castle>();

            unitState = State.fight;

            Debug.Log("enter");
        }
         if(gameObject.tag == collision.gameObject.tag)
        {
            //unitState = State.idle;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "castle")
        {
            Debug.Log("exit");
            enemyCastle = null;
        }
    }

    void AttackUnit()
    {
        feedbacksSoundAttack[Random.Range(0, feedbacksSoundAttack.Length)].Play();
        currentEnemy.LoseLife(attack);
    }

    void AttackCastle()
    {
        Debug.Log("attack");
        feedbacksSoundAttackCastle.Play();
        if (this.gameObject.tag == "player1")
        {
            GameManager.addFury(0.1f, 0);
        }
        else
        {
            GameManager.addFury(0.1f, 1);
        }
        enemyCastle.ReceiveDamage(attack);
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

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

    enum State
    {
        walk,
        fight
    }

    State unitState;

    Unit currentEnemy;

    // Use this for initialization
    void Start () {
        currentTimerAttack = timerAttack;
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
        if (unitState == State.walk)
        {
            this.transform.Translate(this.transform.forward * 0.01f * speed);
        }
    }

    private void LateUpdate()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
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

        }
    }

    void Attack()
    {
        currentEnemy.LoseLife(attack);
    }

    public void LoseLife(int _attack)
    {
        life -= _attack;
    }
}

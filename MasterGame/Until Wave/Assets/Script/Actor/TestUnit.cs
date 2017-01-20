using UnityEngine;
using System.Collections;

public class TestUnit : MonoBehaviour {

    public float speed;
    public float life = 2;
    public float attack = 1;

    public float timerAttack = 3;
    float currentTimerAttack = 0;

    public Transform poolParent;

    enum State
    {
        walk,
        fight
    }

    State unitState;

    GameObject currentEnemy;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (unitState == State.fight)
        {
            if (currentEnemy == null || !currentEnemy.activeSelf)
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
            this.transform.Translate(this.transform.forward * 0.1f * speed);
        }
    }

    private void LateUpdate()
    {
        if (life <= 0)
        {
            transform.position = poolParent.position;
            this.gameObject.SetActive(false);
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        if ((this.gameObject.name == "Unit1" && collision.gameObject.name == "Unit2") || this.gameObject.name == "Unit2" && collision.gameObject.name == "Unit1")
            if (unitState == State.walk)
            {
                currentEnemy = collision.gameObject;
                unitState = State.fight;
            }
    }

    void Attack()
    {
        if (currentEnemy != null && currentEnemy.activeSelf)
        {
            currentEnemy.GetComponent<TestUnit>().LoseLife(attack);
        }
    }

    public void LoseLife(float _attack)
    {
        life -= _attack;
        
    }
}

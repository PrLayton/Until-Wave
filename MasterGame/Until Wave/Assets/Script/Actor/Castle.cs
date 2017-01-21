using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Castle : MonoBehaviour {

    public List<Mesh> meshCastleState;

    [Tooltip("Indique les step du changement de mesh(destruction du chateau)")] 
    public List<uint> limitStateCastle;

    public int health; 


	// Use this for initialization
	void Start () {
	    if(limitStateCastle.Count != meshCastleState.Count)
        {
            Debug.LogAssertion("La liste des mesh doit être égales au nombres d'états");
        }
        else
        {
            //Si le setup est ok on charge le premier mesh 

            UpdateMesh();
        }
	}
    public void ReceiveDamage(int damageValue)
    {
        health -= damageValue;

        Debug.Log("vie "+health);

        UpdateMesh();

        if (health <= 0)
            GameManager.EndGame(true);
    }

    private void UpdateMesh()
    {
        //stock l'index du mesh à charger
        int index = 0;

        for(int i =0; i < limitStateCastle.Count; ++i)
        {
            if (limitStateCastle[i] >= health)
                index = i;
        }

        GetComponent<MeshFilter>().mesh = meshCastleState[index];
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Castle : MonoBehaviour {

    public List<Mesh> meshCastleState;

    [Tooltip("Indique les step du changement de mesh(destruction du chateau)")] 
    public List<uint> limitStateCastle;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

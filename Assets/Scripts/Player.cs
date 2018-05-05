using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static int playerCounter = 0; 
    private int playerNumber_;

    public void Init()
    {
        playerNumber_ = ++playerCounter;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

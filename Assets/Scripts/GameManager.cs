using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

        var foundPlayers = FindObjectsOfType<Player>();
        foreach (var player in foundPlayers)
        {
            player.Init();

            //maybe assign controls here
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	void Awake () {

        var foundPlayers = FindObjectsOfType<Player>();
        foreach (var player in foundPlayers)
        {
            player.Init();
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

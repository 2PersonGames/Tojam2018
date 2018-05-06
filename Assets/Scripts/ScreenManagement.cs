using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManagement : MonoBehaviour {

    Scene scene;
    Canvas canvas;
    Transform playerJoinStatuses;
    int playersConfirmedJoining = 0;

    // Use this for initialization
    void Start () {
        scene = SceneManager.GetActiveScene();

        if (scene.name == "StartChoosePlayers")
        {
            canvas = GameObject.FindObjectOfType<Canvas>();
            playerJoinStatuses = canvas.transform.Find("PlayerJoinStatuses");
        }

    }

    // Update is called once per frame
    void Update () {
        if (scene.name != "StartChoosePlayers") { return; }
        
        for (int p = 1; p <= Player.MAX_PLAYERS; p++)
        {
            if (Input.GetButtonDown(p + "Fire1")) // || Input.GetAxis(p + "Fire1") != 0
            {
                //toggle existing players
                Text playerJoinTextField = playerJoinStatuses.Find("Text_PlayerJoined" + p).GetComponent<Text>();
                if (playerJoinTextField.color == Color.green)
                {
                    playerJoinTextField.color = Color.black;
                    playerJoinTextField.text = "Sitting out.";
                    playersConfirmedJoining--;
                }
                else
                {
                    playerJoinTextField.color = Color.green;
                    playerJoinTextField.text = "Joined!";
                    playersConfirmedJoining++;
                }
            }
        }

        if (Input.GetButtonDown("Submit")) //  || Input.GetAxis(p + "Fire1") != 0
        {
            Debug.Log("Starting game");
            VerifyEnabledPlayersThenBegin();
        }
	}

    private void VerifyEnabledPlayersThenBegin()
    {
        //check if at least 2 players are actively enabled
        if (playersConfirmedJoining >= 2)
        {
            SceneManager.LoadScene("RawPrototypeStage");
            Debug.Log("Switching scene to: " + "RawPrototypeStage");
        }

    }
}

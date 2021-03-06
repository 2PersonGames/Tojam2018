﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    private GameState _currentState;

    public Canvas canvas;
    public Canvas resultsCanvas;
    Transform playerJoinStatuses;
    int playersConfirmedJoining = 0;

    public enum GameState
    {
        Title = 0,
        Active = 1,
        Results = 2
    }

    void Awake () {

        //Check if instance already exists
        if (instance == null)
        {

            //if not, set instance to this
            instance = this;

        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        _currentState = GameState.Title;

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        resultsCanvas = GameObject.Find("ResultsCanvas").GetComponent<Canvas>();
        playerJoinStatuses = canvas.transform.Find("PlayerJoinStatuses");

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    // Update is called once per frame
    void Update () {

        if (_currentState == GameState.Title)
        {

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

            if (Input.GetButtonDown("Submit") && playersConfirmedJoining >= 2)
            {
                Debug.Log("Starting game");
                _currentState = GameState.Active;
                canvas.gameObject.SetActive(false);
            }
        }
        else if (_currentState == GameState.Results)
        {
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("AloneTogether.TOJam2018Edition");
            }
        }

    }

    private void InitGame()
    {
        _currentState = GameState.Title;
        canvas.gameObject.SetActive(true);
        resultsCanvas.gameObject.SetActive(false);

        var foundPlayers = FindObjectsOfType<Player>();
        foreach (var player in foundPlayers)
        {
            player.Init();
        }

    }

    public void SetCurrentState(GameState state)
    {
        _currentState = state;
    }

    public GameState getCurrentState()
    {
        return _currentState;
    }

    public bool isGameStateActive()
    {
        return _currentState == GameState.Active;
    }
}

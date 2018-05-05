using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private static int playerCounter = 0;
    private int playerNumber_;

    public void Init()
    {
        playerNumber_ = ++playerCounter;
        gameObject.AddComponent<IanMovementController>();
        var clusteringSystem = gameObject.AddComponent<ClusteringSystem>();

        var throwController = gameObject.AddComponent<ThrowController>();
        throwController.ClusteringSystem = clusteringSystem;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

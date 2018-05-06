﻿using System.Linq;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private const float FORCE = 500.0f;
    private static byte[] _proceduralHappinessValues = new byte[8];

    public GameObject ThrownObjectPrefab;

    private Player _player;
    private int _proceduralHappinessValuesCurrentIndex;
    private Vector2 _lastThrownDirection;
    
    // Use this for initialization
    void Start()
    {
        _player = gameObject.GetComponent<Player>();

        lock (_proceduralHappinessValues)
        {
            if (_proceduralHappinessValues[0] == 0)
            {
                for (int i = 0; i < _proceduralHappinessValues.Length; i++)
                {
                    _proceduralHappinessValues[i] = (byte)Random.Range(1, 3);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _player.GetCountOfHappinessOwnedByMe() == 0)
        {
            var direction = GetDirection();
            if (direction == Vector2.zero)
            {
                direction = _lastThrownDirection;
                if (direction == Vector2.zero)
                {
                    var opponentPlayer = Resources.FindObjectsOfTypeAll<Player>().First(obj => obj != _player);
                    direction = opponentPlayer.gameObject.transform.position - _player.gameObject.transform.position;
                }
            }

            direction.Normalize();

            var happiness = Instantiate(ThrownObjectPrefab, gameObject.transform.position, Quaternion.identity);
            var happinessController = happiness.GetComponent<HappinessController>();
            happinessController.OriginPlayer = _player;

            var happinessRigidBody2D = happiness.GetComponent<Rigidbody2D>();
            happinessRigidBody2D.velocity = _player.GetComponent<Rigidbody2D>().velocity;
            happinessRigidBody2D.AddForce(
                (direction * Time.deltaTime * FORCE),
                ForceMode2D.Impulse);

            happinessController.Happiness = _proceduralHappinessValues[_proceduralHappinessValuesCurrentIndex];
            _proceduralHappinessValuesCurrentIndex = Mathf.Clamp(_proceduralHappinessValuesCurrentIndex++, 0, _proceduralHappinessValues.Length - 1);

            _player.HappinessCreated(happinessController);

            _lastThrownDirection = direction;
        }
    }

    private Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis("RightH"), Input.GetAxis("RightV"));
    }
}

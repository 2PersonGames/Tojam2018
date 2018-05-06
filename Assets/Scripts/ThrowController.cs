﻿using System.Linq;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public const byte MAX_HAPPINESS_THROW = 3;
    private const float FORCE = 0.065f;
    private const float THROW_COOLDOWN_LENGTH = 0.75f;
    private static byte[] _proceduralHappinessValues = new byte[8];

    public GameObject ThrownObjectPrefab;

    private Player _player;
    private int _proceduralHappinessValuesCurrentIndex;
    private Vector2 _lastThrownDirection;
    private float _throwCooldown;

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
                    _proceduralHappinessValues[i] = (byte)Random.Range(1, MAX_HAPPINESS_THROW);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // cooldown logic
        if (_throwCooldown > 0.0f)
        {
            _throwCooldown -= Time.deltaTime;
        }
        
        if (_throwCooldown <= 0.0f
            && (Input.GetButtonDown(_player.GetInputName("Fire1")) || Input.GetAxis(_player.GetInputName("Fire1")) > 0.0f)
            && _player.GetCountOfHappinessOwnedByMe() < 3
            && _player.GetState() != Player.State.NoHappinessLeft)
        {
            var direction = GetDirection();
            if (direction == Vector2.zero)
            {
                direction = _lastThrownDirection;
                if (direction == Vector2.zero)
                {
                    var opponentPlayer = Resources.FindObjectsOfTypeAll<Player>().First(obj => obj != _player);
                    direction = opponentPlayer.gameObject.transform.position - _player.gameObject.transform.position;
                    if (direction == Vector2.zero)
                    {
                        direction = Vector2.down;
                    }
                }
            }

            Debug.Log("before norm -- X: " + direction.x + " | y: " + direction.y);
            direction.Normalize();
            Debug.Log("after -- X: " + direction.x + " | y: " + direction.y);

            var position = gameObject.transform.position;
            var distanceAwayFromCollision = direction * 0.5f 
                * ((Vector2.SqrMagnitude(_player.GetComponent<BoxCollider2D>().size))
                    + (Vector2.SqrMagnitude(ThrownObjectPrefab.GetComponent<BoxCollider2D>().size)));
            var playerVelocity = _player.GetComponent<Rigidbody2D>().velocity;
            distanceAwayFromCollision += playerVelocity * Time.fixedDeltaTime * 2.0f;
            position += new Vector3(distanceAwayFromCollision.x, distanceAwayFromCollision.y, 0.0f);

            var happiness = Instantiate(ThrownObjectPrefab, position, Quaternion.identity);
            var happinessController = happiness.GetComponent<HappinessController>();
            happinessController.OriginPlayer = _player;

            var happinessRigidBody2D = happiness.GetComponent<Rigidbody2D>();
            happinessRigidBody2D.velocity = playerVelocity;
            happinessRigidBody2D.AddForce(
                (direction * Time.deltaTime * FORCE),
                ForceMode2D.Impulse);

            happinessController.Happiness = _proceduralHappinessValues[_proceduralHappinessValuesCurrentIndex];
            _proceduralHappinessValuesCurrentIndex = Mathf.Clamp(_proceduralHappinessValuesCurrentIndex++, 0, _proceduralHappinessValues.Length - 1);

            _player.HappinessCreated(happinessController);

            _lastThrownDirection = direction;
            _throwCooldown = THROW_COOLDOWN_LENGTH;
        }
    }

    private Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis(_player.GetInputName("FireDirectionH")), Input.GetAxis(_player.GetInputName("FireDirectionV")));
    }
}

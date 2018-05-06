using System;
using System.Linq;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public const byte MAX_HAPPINESS_THROW = 3;
    private const float FORCE = 0.065f;
    private const float THROW_COOLDOWN_LENGTH = 0.75f;

    private static readonly byte[] _proceduralHappinessValues;

    public Sprite[] ThrowingSprites;
    public GameObject AimingPrefab;
    public GameObject ThrownObjectPrefab;

    private Player _player;
    private int _proceduralHappinessValuesCurrentIndex;
    private Vector2 _lastThrownDirection;
    private float _throwCooldown;

    static ThrowController()
    {
        _proceduralHappinessValues = new byte[]
        {
            1, 2, 2, 3, 1, 2, 3, 1, 2
        };
    }

    // Use this for initialization
    void Start()
    {
        _player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameStateActive()) { return; }

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
            var position = gameObject.transform.position;
            var distanceAwayFromCollision = direction 
                * ((_player.GetComponent<CircleCollider2D>().radius)
                    + (ThrownObjectPrefab.GetComponent<CircleCollider2D>().radius));
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

            var happinessAmount = _proceduralHappinessValues[_proceduralHappinessValuesCurrentIndex];
            happinessController.Happiness = happinessAmount;
            happiness.GetComponent<SpriteRenderer>().sprite = ThrowingSprites[happinessAmount - 1];
            _proceduralHappinessValuesCurrentIndex++;
            if (_proceduralHappinessValuesCurrentIndex >= _proceduralHappinessValues.Length)
            {
                _proceduralHappinessValuesCurrentIndex = 0;
            }

            _player.HappinessCreated(happinessController);
            _lastThrownDirection = direction;
            _throwCooldown = THROW_COOLDOWN_LENGTH;
        }
    }

    private Vector2 GetDirection()
    {
        var direction = new Vector2(
            Input.GetAxis(_player.GetInputName("FireDirectionH")),
            Input.GetAxis(_player.GetInputName("FireDirectionV")));

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

        direction.Normalize();

        return direction;
    }
}

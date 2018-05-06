using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private const float FORCE = 500.0f;

    public GameObject ThrownObjectPrefab;

    private Player _player;

    // Use this for initialization
    void Start()
    {
        _player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
            var direction = GetDirection();
            if (direction != Vector2.zero && Input.GetButtonDown("Fire1") && _player.GetCountOfHappinessOwnedByMe() == 0)
            {
                var happiness = Instantiate(ThrownObjectPrefab, gameObject.transform.position, Quaternion.identity);
                happiness.GetComponent<HappinessController>().OriginPlayer = _player;

                var happinessRigidBody2D = happiness.GetComponent<Rigidbody2D>();
                happinessRigidBody2D.velocity = _player.GetComponent<Rigidbody2D>().velocity;
                happinessRigidBody2D.AddForce(
                    (direction * Time.deltaTime * FORCE),
                    ForceMode2D.Impulse);

                _player.BlobCreated(happiness);
            }
    }

    private Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis("RightH"), Input.GetAxis("RightV"));
    }
}

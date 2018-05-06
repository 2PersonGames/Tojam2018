using UnityEngine;

public class MovementController : MonoBehaviour
{
    private const float multiplier = 10.0f;
    //private const float multiplier = 100.0f;


    private Rigidbody2D _rigidbody2D;
    private Player _player;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis(_player.GetInputName("Horizontal"));
        float y = Input.GetAxis(_player.GetInputName("Vertical"));
        Vector2 delta = new Vector2(x, y);

        if (delta != Vector2.zero)
        {
            Vector2 currentPosition = _rigidbody2D.position;

            _rigidbody2D.MovePosition(new Vector2(currentPosition.x + delta.x * Time.deltaTime * multiplier, currentPosition.y + delta.y * Time.deltaTime * multiplier));
        }
    }
}


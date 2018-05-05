using UnityEngine;

public class MovementController : MonoBehaviour
{
    private const float FORCE = 50.0f;
    private Rigidbody2D _rigidbody2D;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 inputs = new Vector2(x, y);

        if (inputs != Vector2.zero)
        {
            _rigidbody2D.AddForce(new Vector2(inputs.x, inputs.y) * Time.deltaTime * FORCE);
        }
    }
}

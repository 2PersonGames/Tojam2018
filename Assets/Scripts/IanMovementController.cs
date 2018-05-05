using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IanMovementController : MonoBehaviour
{
    private float force = 50f;
    private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start ()
	{
	    rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
	    rigidbody2D.gravityScale = 0;

	    gameObject.AddComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update ()
	{
	    float x = Input.GetAxis("Horizontal");
	    float y = Input.GetAxis("Vertical");
        Vector2 inputs = new Vector2(x,y);

        if (inputs.x != 0 || inputs.y != 0)
	    {
            rigidbody2D.AddForce( new Vector2(inputs.x, inputs.y) * Time.deltaTime * force);
	    }

	}
}

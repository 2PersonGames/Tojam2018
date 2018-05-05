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
	    float horizontalInput = Input.GetAxis("Horizontal");


	    if (horizontalInput != 0)
	    {
            rigidbody2D.AddForce(Vector2.right * horizontalInput * Time.deltaTime * force);
	    }

	}
}

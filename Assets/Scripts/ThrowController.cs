using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private const float FORCE = 50.0f;

    public ClusteringSystem ClusteringSystem { private get; set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetButtonDown("Fire1"))
        {
            var blob = ClusteringSystem.GetBlob();
            var rigidBody2D = blob.GetComponent<Rigidbody2D>();
            rigidBody2D.AddForce(GetDirection() * Time.deltaTime * FORCE);
        }
    }

    private Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis("RightH"), Input.GetAxis("RightV"));
    }
}

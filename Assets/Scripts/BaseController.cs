using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public Rigidbody RigidBody { get; private set; }

    // Use this for initialization
    protected virtual void Start()
    {
        RigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected abstract void Update();

    protected virtual void FixedUpdate()
    {
        // Check there is movement to be applied
        var direction = GetMovementDirection();
        if (direction != Vector3.zero)
        {
            // Apply movement by modifying the velocity
            var movementSpeed = GetMovementSpeed();
            RigidBody.velocity = new Vector3(
                direction.x * movementSpeed,
                RigidBody.velocity.y,
                direction.z * movementSpeed);

            // Rotate the object in the direction we're now moving
            if (direction.x < 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
            else if (direction.x > 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
            }
        }
    }

    protected virtual float GetMovementSpeed()
    {
        return 1.0f;
    }

    protected abstract Vector3 GetMovementDirection();
}

using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public Rigidbody2D RigidBodyTwoDimensional { get; private set; }

    // Use this for initialization
    protected virtual void Start()
    {
        RigidBodyTwoDimensional = gameObject.GetComponent<Rigidbody2D>();
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
            RigidBodyTwoDimensional.AddForce(
                new Vector2(direction.x * movementSpeed, direction.y * movementSpeed),
                ForceMode2D.Impulse);

            // Rotate the object in the direction we're now moving
            if (false)
            {
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
    }

    protected virtual float GetMovementSpeed()
    {
        return 5.0f;
    }

    protected abstract Vector3 GetMovementDirection();
}

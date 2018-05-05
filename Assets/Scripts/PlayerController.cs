using UnityEngine;

public class PlayerController : BaseController
{
    protected override void Update()
    {
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override Vector3 GetMovementDirection()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            direction += Vector3.right;
        }
        else if (Input.GetAxis("Horizontal") < -0.1f)
        {
            direction += Vector3.left;
        }
        if (Input.GetAxis("Vertical") < -0.1f)
        {
            direction += Vector3.down;
        }
        else if (Input.GetAxis("Vertical") > 0.1f)
        {
            direction += Vector3.up;
        }

        return Vector3.Normalize(direction);
    }
}

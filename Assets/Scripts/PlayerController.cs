using UnityEngine;

public class PlayerController : BaseController
{
    protected override void Update()
    {
        throw new System.NotImplementedException();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override Vector3 GetMovementDirection()
    {
        if (RigidBody.velocity.y <= 0.01f && RigidBody.velocity.y >= -0.01f)
        {
            Vector3 diection = Vector3.zero;

            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                diection += Vector3.right;
            }
            else if (Input.GetAxis("Horizontal") < -0.1f)
            {
                diection += Vector3.left;
            }
            else if (Input.GetAxis("Vertical") < -0.1f)
            {
                diection += Vector3.forward;
            }
            else if (Input.GetAxis("Vertical") > 0.1f)
            {
                diection += Vector3.back;
            }

            return Vector3.Normalize(diection);
        }
        else
        {
            return Vector3.zero;
        }
    }
}

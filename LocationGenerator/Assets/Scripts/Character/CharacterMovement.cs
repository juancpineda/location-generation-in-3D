//----------------------------------------------------------------------------------------------------------------------------------------
// The CharacterMovement class is a script that moves a character using a raycast to stop it if it finds an obstacle in the direction of its movement
//----------------------------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 10f;

    public float rayDistance = 1f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;

        movement = CheckRays(movement);

        transform.position += movement;

        AdjustScale(horizontal);
    }

    Vector3 CheckRays(Vector3 movement)
    {
        Ray forwardRay = new Ray(transform.position, Vector3.forward);
        Ray backwardRay = new Ray(transform.position, Vector3.back);
        Ray leftRay = new Ray(transform.position, Vector3.left);
        Ray rightRay = new Ray(transform.position, Vector3.right);

        RaycastHit hit;

        if (Physics.Raycast(forwardRay, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                movement.z = Mathf.Min(movement.z, 0);
            }
        }

        if (Physics.Raycast(backwardRay, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                movement.z = Mathf.Max(movement.z, 0);
            }
        }

        if (Physics.Raycast(leftRay, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                movement.x = Mathf.Max(movement.x, 0);
            }
        }

        if (Physics.Raycast(rightRay, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                movement.x = Mathf.Min(movement.x, 0);
            }
        }

        return movement;
    }

    void AdjustScale(float horizontal)
    {
        if (horizontal > 0f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontal < 0f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}

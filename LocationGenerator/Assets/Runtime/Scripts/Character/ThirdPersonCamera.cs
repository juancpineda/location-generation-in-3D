using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject player; 
    public float distance = 10.0f; 
    public float height = 5.0f; 
    public float damping = 5.0f; 

    private Vector3 offset; 

    void Start()
    {
        offset = new Vector3(0, height, -distance);
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;

        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);

        transform.position = position;

        transform.LookAt(player.transform.position);
    }
}

using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        float xRotation = transform.rotation.eulerAngles.x;
        float zRotation = transform.rotation.eulerAngles.z;

        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);

        transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, zRotation);
    }
}

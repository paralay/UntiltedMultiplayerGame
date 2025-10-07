using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    float xRotation = 0;
    float sensitivity = 100f;

    [SerializeField] GameObject parent;
    void Start()
    {
        xRotation = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            parent.transform.Rotate(Vector3.up * mouseX);
        }
    }
}

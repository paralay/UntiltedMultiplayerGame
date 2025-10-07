using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    float xRotation = 0;
    float sensitivity = 300f;

    [SerializeField] GameObject parent;

    bool isActive = false;
    void Start()
    {

        xRotation = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isActive = !isActive;
            if (isActive)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if (isActive)
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

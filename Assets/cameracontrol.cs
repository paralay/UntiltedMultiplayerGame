using UnityEngine;

public class cameracontrol : MonoBehaviour
{

    [SerializeField] GameObject player;

    bool isActive = false;
    bool inMenu = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inMenu = !inMenu;
            if (isActive)
            {
                isActive = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(inMenu) return;
            isActive = true;
            if (isActive)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (isActive)
        {
            float mouseX = Input.GetAxis("Mouse X") * clientData.sensibility * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * clientData.sensibility * Time.deltaTime;

            float xRotation = 0;

            xRotation -= mouseY;

            transform.RotateAround(player.transform.position, player.transform.right, xRotation);

            player.transform.Rotate(Vector3.up * mouseX);
        }
    }
}

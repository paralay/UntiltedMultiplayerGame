using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] private GameObject player;

    bool isActive = false;
    bool inMenu = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inMenu = !inMenu; //Could have been better
            LockCamera();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(inMenu) return;
            UnlockCamera();
        }

        if (isActive) MoveCameraOnAxis();
    }

    private void LockCamera()
    {
        isActive = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UnlockCamera()
    {
        isActive = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void MoveCameraOnAxis()
    {
        float lMouseX = Input.GetAxis("Mouse X") * ClientData.sensibility * Time.deltaTime;
        float lMouseY = Input.GetAxis("Mouse Y") * ClientData.sensibility * Time.deltaTime;

        float lXRotation = 0;

        lXRotation -= lMouseY;

        transform.RotateAround(player.transform.position, player.transform.right, lXRotation);

        player.transform.Rotate(Vector3.up * lMouseX);
    }
}
